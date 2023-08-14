using GamesAPI.DTOs.Auth;
using GamesAPI.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Claims;

namespace GamesAPI.Services
{
    public interface IAuthService
    {
        public Task<bool> Login(LoginDTO dto);
        public Task<bool> Logout(string token);
    }
}
