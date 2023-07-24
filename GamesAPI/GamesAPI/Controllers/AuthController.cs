using GamesAPI.DTOs;
using GamesAPI.Models;
using GamesAPI.Responses;
using GamesAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public AuthController(IAuthService authService, IUserService userService,
            ITokenService tokenService)
        {
            _authService = authService;
            _userService = userService;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public ActionResult LogIn(LoginDTO dto)
        {
            if (_authService.Login(dto))
            {
                AppUser? userData = _userService.FindByEmail(dto.Email);
                if (userData != null)
                {
                    try
                    {
                        List<IdentityRole<int>> roles = _userService.GetRolesByUser(userData);
                        string token = _tokenService.GenerateJSONWebToken(userData, roles);
                        string refreshToken;
                        if (userData.Expires < DateTime.Now)
                        {
                            refreshToken = _tokenService.GenerateRefreshTokenString();
                            _userService.GenerateRefreshToken(userData, refreshToken, 7);
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
        public ActionResult RefreshToken(RefreshTokenDTO dto)
        {
            if (dto == null)
                return BadRequest("Dto parameters are null");
            try
            {
                AuthenticationResponse? response = _tokenService.RefreshToken(dto);
                if (response != null)
                {
                    return Ok(response);
                }
            }
            catch
            {
                return BadRequest("Cannot refresh token");
            }
            return BadRequest("Cannot refresh token");
        }
        [HttpPost("validate")]
        public ActionResult IsTokenValid([FromBody] ValidateTokenDTO dto)
        {
            if (dto.Token != null)
            {
                if (_tokenService.ValidateToken(dto.Token))
                {
                    return Ok();
                }
            }
            return Unauthorized();
        }
    }
}

