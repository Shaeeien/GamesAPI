using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly UserManager<AppUser> _manager;
        private readonly ApplicationDbContext _context;
        public UserTokenService(UserManager<AppUser> manager, ApplicationDbContext context)
        {
            _manager = manager;
            _context = context;
        }
        public async Task<bool> Add(IdentityUserToken<int> tokenToAdd)
        {
            try
            {
                await _context.UserTokens.AddAsync(tokenToAdd);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IdentityUserToken<int>?> FindByUserId(int userId)
        {
            return await _context.UserTokens.Where(t => t.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IdentityUserToken<int>?> FindByUserTokenString(string tokenString)
        {
            return await _context.UserTokens.Where(t => t.Value == tokenString).FirstOrDefaultAsync();
        }

        public async Task<bool> Remove(IdentityUserToken<int> tokenToRemove)
        {
            try
            {
                _context.UserTokens.Remove(tokenToRemove);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(IdentityUserToken<int> tokenToUpdate)
        {
            try
            {

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
