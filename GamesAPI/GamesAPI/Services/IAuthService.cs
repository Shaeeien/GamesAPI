using GamesAPI.DTOs;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Claims;

namespace GamesAPI.Services
{
    public interface IAuthService
    {
        public bool Login(LoginDTO dto);
        public void Logout();
    }
}
