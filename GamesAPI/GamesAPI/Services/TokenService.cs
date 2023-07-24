using GamesAPI.DTOs;
using GamesAPI.Models;
using GamesAPI.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace GamesAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public TokenService(IConfiguration configuration, 
            IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public string GenerateJSONWebToken(AppUser userInfo, List<IdentityRole<int>> userRoles)
        {
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new Claim("id", userInfo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userInfo.UserName.ToString())
                };
                foreach (IdentityRole<int> role in userRoles)
                {
                    if (role.Name != null)
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                  _configuration["Jwt:Audience"],
                  claims,
                  expires: DateTime.Now.AddMinutes(20),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }

        public string GenerateRefreshTokenString()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        public bool IsTokenActive(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false
            };
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                if(validatedToken.ValidTo < DateTime.Now.AddMinutes(20))
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public AuthenticationResponse? RefreshToken(RefreshTokenDTO dto)
        {
            if (dto is null)
            {
                return null;
            }                
            if (dto.Token != null)
            {
                var principal = GetPrincipalFromToken(dto.Token);
                var userName = principal.Identity.Name;

                var user = _userService.FindByName(userName);
                var roles = _userService.GetRolesByUser(user);

                if (user != null)
                {
                    var newToken = GenerateJSONWebToken(user, roles);
                    if(user.Expires < DateTime.Now)
                    {
                        var refreshTokenString = GenerateRefreshTokenString();
                        user.RefreshToken = refreshTokenString;
                    }
                    
                    user.TokenCreatedAt = DateTime.Now;
                    user.Expires = DateTime.Now.AddDays(7);
                    _userService.SaveChanges();
                    return new AuthenticationResponse()
                    {
                        Token = newToken,
                        RefreshToken = user.RefreshToken
                    };
                }
            }
            return null;
        }

        public bool RemoveJWT(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false
                }, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RefreshToken(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);
            if (validatedToken == null)
            {
                return false;
            }
            var userName = validatedToken.Claims.Single(c => c.Type == ClaimTypes.Name).Value;
            if (userName != null)
            {
                return true;
            }
            return false;
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false
            };
            try
            {
                var principal = tokenHandler.ValidateToken(token, parameters, out var validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
