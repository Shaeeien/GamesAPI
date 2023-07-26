using GamesAPI.DTOs.Auth;
using GamesAPI.Models;
using GamesAPI.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GamesAPI.Services
{
    public interface ITokenService
    {
        public string GenerateJSONWebToken(AppUser userInfo, List<IdentityRole<int>> userRoles);        
        public bool RemoveJWT(AppUser user);
        public bool IsTokenActive(string token);
        public Task<AuthenticationResponse?> RefreshToken(RefreshTokenDTO dto);
        public string GenerateRefreshTokenString();
        public bool ValidateToken(string token);
        public bool RefreshToken(string token, string refreshToken);
        public ClaimsPrincipal GetPrincipalFromToken(string token);

    }
}
