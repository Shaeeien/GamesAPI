using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GamesAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _authContext;
        public UserService(ApplicationDbContext ctx) {
            _authContext = ctx;
        }

        public bool Add(AppUser userToAdd)
        {
            try
            {
                _authContext.Users.Add(userToAdd);
                _authContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public AppUser? FindById(int id)
        {
            return _authContext.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public AppUser? FindByEmail(string email)
        {
            return _authContext.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public bool Remove(AppUser userToRemove)
        {
            var removedUser = _authContext.Users.Remove(userToRemove);
            if(removedUser != null)
            {
                _authContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(AppUser userToUpdate, AppUser updatedUser)
        {
            try
            {
                userToUpdate.UserName = updatedUser.UserName;
                userToUpdate.Email = updatedUser.Email;
                userToUpdate.PasswordHash = updatedUser.PasswordHash;
                _authContext.SaveChanges();
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

        public List<AppUser> GetAllUsers()
        {
            return _authContext.Users.ToList();
        }

        public bool GenerateRefreshToken(AppUser user, string token, int days)
        {
            if(user != null)
            {
                try
                {
                    user.RefreshToken = token;
                    user.Expires = DateTime.Now.AddDays(days);
                    user.TokenCreatedAt = DateTime.Now;
                    _authContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public AppUser? FindByName(string name)
        {
            return _authContext.Users.Where(u => u.UserName == name).FirstOrDefault();
        }

        public int SaveChanges()
        {
            return _authContext.SaveChanges();
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
