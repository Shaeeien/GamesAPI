using GamesAPI.DTOs.Auth;
using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace GamesAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public AuthService(IUserService userService, IConfiguration configuration) 
        {
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<bool> Login(LoginDTO dto)
        {
            IdentityUser<int>? user = await _userService.FindByEmail(dto.Email);
            if(user != null && BC.Verify(dto.Password, user.PasswordHash))
                return true;
            return false;
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
