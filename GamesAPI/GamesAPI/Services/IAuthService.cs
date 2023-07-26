using GamesAPI.DTOs.Auth;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Claims;

namespace GamesAPI.Services
{
    public interface IAuthService
    {
        public Task<bool> Login(LoginDTO dto);
        public void Logout();
    }
}
