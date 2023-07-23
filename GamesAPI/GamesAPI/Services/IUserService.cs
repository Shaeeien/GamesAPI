using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GamesAPI.Services
{
    public interface IUserService
    {
        public bool Add(AppUser userToAdd);
        public bool Remove(AppUser userToRemove);
        public bool Update(AppUser userToUpdate, AppUser updatedUser);
        public AppUser? FindByEmail(string email);
        public AppUser? FindById(int id);
        public List<IdentityRole<int>> GetRolesByUser(AppUser user);
        public List<AppUser> GetAllUsers();
        public bool GenerateRefreshToken(AppUser user, string token, int days);
        public AppUser? FindByName(string name);
        public int SaveChanges();
        public bool Exists(AppUser user);
        public bool IsEmailTaken(string email);
        public bool IsUserNameTaken(string userName);
    }
}
