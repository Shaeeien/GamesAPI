using GamesAPI.DTOs.Auth;
using GamesAPI.Models;
using GamesAPI.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserTokenService _userTokenService;
        private readonly ApplicationDbContext _context;

        public TokenService(IConfiguration configuration, 
            IUserService userService,
            IUserTokenService userTokenService,
            ApplicationDbContext context)
        {
            _configuration = configuration;
            _userService = userService;
            _userTokenService = userTokenService;
            _context = context;
        }

        public async Task<string> GenerateJSONWebToken(AppUser userInfo, List<IdentityRole<int>> userRoles)
        {
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new Claim("id", userInfo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userInfo.UserName),
                    new Claim(ClaimTypes.Email, userInfo.Email)
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

                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                if (await SaveTokenInDatabase(userInfo, tokenString))
                    return tokenString;
                return null;
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

        public async Task<AuthenticationResponse?> RefreshToken(string token)
        {
            if (token is null)
            {
                return null;
            }                
            if (token != null)
            {
                var principal = GetPrincipalFromToken(token);
                var userName = principal?.Identity?.Name;

                var user = await _userService.FindByName(userName);
                var roles = _userService.GetRolesByUser(user);

                if (user != null)
                {
                    var newToken = await GenerateJSONWebToken(user, roles);
                    if(user.Expires < DateTime.Now)
                    {
                        var refreshTokenString = GenerateRefreshTokenString();
                        user.RefreshToken = refreshTokenString;
                    }
                    
                    user.TokenCreatedAt = DateTime.Now;
                    user.Expires = DateTime.Now.AddDays(7);
                    await _userService.SaveChanges();
                    return new AuthenticationResponse()
                    {
                        Token = newToken,
                        RefreshToken = user.RefreshToken
                    };
                }
            }
            return null;
        }


        public async Task<bool> SaveTokenInDatabase(AppUser userInfo, string token)
        {
            try
            {
                IdentityUserToken<int>? tokenToRemove = await _context.UserTokens.Where(t => t.UserId == userInfo.Id).FirstOrDefaultAsync();
                if (tokenToRemove != null)
                {
                    _context.UserTokens.Remove(tokenToRemove);
                }
                await _context.UserTokens.AddAsync(new IdentityUserToken<int>()
                {
                    UserId = userInfo.Id,
                    LoginProvider = "localhost:5120",
                    Name = userInfo.UserName,
                    Value = token.ToString()
                });
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveJWT(string token)
        {
            try
            {
                IdentityUserToken<int>? tokenToRemove = await _userTokenService.FindByUserTokenString(token);
                if (tokenToRemove != null)
                {
                    await _userTokenService.Remove(tokenToRemove);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
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
