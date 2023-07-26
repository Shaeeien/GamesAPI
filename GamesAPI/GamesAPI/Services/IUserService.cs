using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GamesAPI.Services
{
    public interface IUserService
    {
        public Task<bool> Add(AppUser userToAdd);
        public Task<bool> Remove(AppUser userToRemove);
        public Task<bool> Update(AppUser userToUpdate, AppUser updatedUser);
        public Task<AppUser?> FindByEmail(string email);
        public Task<AppUser?> FindById(int id);
        public List<IdentityRole<int>> GetRolesByUser(AppUser user);
        public Task<List<AppUser>> GetAllUsers();
        public Task<bool> GenerateRefreshToken(AppUser user, string token, int days);
        public Task<AppUser?> FindByName(string name);
        public Task<int> SaveChanges();
        public bool Exists(AppUser user);
        public bool IsEmailTaken(string email);
        public bool IsUserNameTaken(string userName);
    }
}
