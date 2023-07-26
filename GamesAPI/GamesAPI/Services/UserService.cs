using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _authContext;
        public UserService(ApplicationDbContext ctx) {
            _authContext = ctx;
        }

        public async Task<bool> Add(AppUser userToAdd)
        {
            try
            {
                await _authContext.Users.AddAsync(userToAdd);
                _authContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<AppUser?> FindById(int id)
        {
            return await _authContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<AppUser?> FindByEmail(string email)
        {
            return await _authContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> Remove(AppUser userToRemove)
        {
            var removedUser = _authContext.Users.Remove(userToRemove);
            if(removedUser != null)
            {
                await _authContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Update(AppUser userToUpdate, AppUser updatedUser)
        {
            try
            {
                userToUpdate.UserName = updatedUser.UserName;
                userToUpdate.Email = updatedUser.Email;
                userToUpdate.PasswordHash = updatedUser.PasswordHash;
                await _authContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }            
        }

        public List<IdentityRole<int>> GetRolesByUser(AppUser user)
        {
            List<IdentityUserRole<int>> userRoles = _authContext.UserRoles.Where(r => r.UserId == user.Id).ToList();
            List<IdentityRole<int>> roles = new List<IdentityRole<int>>();

            foreach(IdentityUserRole<int> role in userRoles)
            {
                IdentityRole<int>? roleToAdd = _authContext.Roles.Where(r => r.Id == role.RoleId).FirstOrDefault();
                if(roleToAdd != null)
                {
                    roles.Add(roleToAdd);
                }
            }
            return roles;
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            return await _authContext.Users.ToListAsync();
        }

        public async Task<bool> GenerateRefreshToken(AppUser user, string token, int days)
        {
            if(user != null)
            {
                try
                {
                    user.RefreshToken = token;
                    user.Expires = DateTime.Now.AddDays(days);
                    user.TokenCreatedAt = DateTime.Now;
                    await _authContext.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<AppUser?> FindByName(string name)
        {
            return await _authContext.Users.Where(u => u.UserName == name).FirstOrDefaultAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _authContext.SaveChangesAsync();
        }

        public bool Exists(AppUser user)
        {
            return _authContext.Users.Where(u => u.Email == user.Email || u.UserName == user.UserName).FirstOrDefault() != null;
        }

        public bool IsEmailTaken(string email)
        {
            return _authContext.Users.Where(u => u.Email == email).FirstOrDefault() is not null;
        }

        public bool IsUserNameTaken(string userName)
        {
            return _authContext.Users.Where(u => u.UserName == userName).FirstOrDefault() is not null;
        }
    }
}
