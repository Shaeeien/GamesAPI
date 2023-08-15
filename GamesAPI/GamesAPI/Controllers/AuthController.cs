using Azure.Core;
using GamesAPI.DTOs.Auth;
using GamesAPI.Models;
using GamesAPI.Responses;
using GamesAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamesAPI.Controllers
{
    [ApiController]
    [EnableCors("ReactApp")]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IUserTokenService _userTokenService;
        public AuthController(IAuthService authService, IUserService userService,
            ITokenService tokenService, IUserTokenService userTokenService)
        {
            _authService = authService;
            _userService = userService;
            _tokenService = tokenService;
            _userTokenService = userTokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LogIn(LoginDTO dto)
        {
            if (await _authService.Login(dto))
            {
                AppUser? userData = await _userService.FindByEmail(dto.Email);
                if (userData != null)
                {
                    try
                    {
                        List<IdentityRole<int>> roles = _userService.GetRolesByUser(userData);
                        string token = await _tokenService.GenerateJSONWebToken(userData, roles);
                        string refreshToken;
                        if (userData.Expires < DateTime.Now)
                        {
                            refreshToken = _tokenService.GenerateRefreshTokenString();
                            await _userService.GenerateRefreshToken(userData, refreshToken, 7);
                            Response.Cookies.Append("refreshToken", refreshToken);
                            return Ok(new AuthenticationResponse
                            {
                                Token = token,
                                RefreshToken = refreshToken
                            });
                        }
                        else
                        {
                            Response.Cookies.Append("refreshToken", userData.RefreshToken);
                            return Ok(new AuthenticationResponse
                            {
                                Token = token,
                                RefreshToken = userData.RefreshToken
                            });
                        }

                    }
                    catch
                    {
                        return Unauthorized();
                    }

                }
            }
            return Unauthorized();
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshToken()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if(token != null)
            {
                try
                {
                    AuthenticationResponse? response = await _tokenService.RefreshToken(token);
                    if (response != null)
                    {
                        return Ok(response);
                    }
                }
                catch
                {
                    return BadRequest("Cannot refresh token");
                }
            }            
            return BadRequest("Cannot refresh token");
        }

        /// <summary>
        /// SPRAWDZIĆ, CZY TOKEN JEST W BAZIE!
        /// </summary>
        /// <returns></returns>
        [HttpPost("validate")]
        public async Task<ActionResult> IsTokenValid()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (token != null)
            {
                if (await _userTokenService.FindByUserTokenString(token) != null)
                {
                
                    try
                    {
                        _tokenService.ValidateToken(token);
                        return Ok();
                    }
                    catch
                    {
                        return Unauthorized("Token is not valid");
                    }
                }
            }
            
            return Unauthorized("Token does not exist in database");
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (token != null)
            {
                try
                {
                    await _authService.Logout(token);
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
           
            return Unauthorized();
        }
    }
}

