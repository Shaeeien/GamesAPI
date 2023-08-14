using Microsoft.AspNetCore.Identity;

namespace GamesAPI.Services
{
    public interface IUserTokenService
    {
        Task<bool> Add(IdentityUserToken<int> tokenToAdd);
        Task<bool> Remove(IdentityUserToken<int> tokenToRemove);
        Task<bool> Update(IdentityUserToken<int> tokenToUpdate);
        Task<IdentityUserToken<int>?> FindByUserId(int userId);
        Task<IdentityUserToken<int>?> FindByUserTokenString(string tokenString);
    }
}
