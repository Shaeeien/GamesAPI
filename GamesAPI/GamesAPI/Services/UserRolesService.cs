using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GamesAPI.Services
{
    public class UserRolesService : IUserRolesService
    {
        private readonly ApplicationDbContext _context;
        public UserRolesService(ApplicationDbContext ctx) 
        { 
            _context = ctx;
        }
        public bool Add(IdentityUserRole<int> roleToAdd)
        {
            try
            {
                _context.UserRoles.Add(roleToAdd);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<IdentityUserRole<int>>? FindByUserId(int id)
        {
            return _context.UserRoles.Where(ur => ur.UserId == id).ToList();
        }

        public List<IdentityUserRole<int>>? FindByRoleId(int id)
        {
            return _context.UserRoles.Where(ur => ur.RoleId == id).ToList();
        }

        public List<IdentityUserRole<int>> GetAll()
        {
            return _context.UserRoles.ToList();
        }

        public bool Remove(IdentityUserRole<int> roleToRemove)
        {
            var removedUserRole = _context.UserRoles.Remove(roleToRemove);
            try
            {
                if (removedUserRole != null)
                {
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }            
        }

        public bool Update(IdentityUserRole<int> roleToUpdate, IdentityUserRole<int> updatedRole)
        {
            try
            {
                roleToUpdate.UserId = updatedRole.UserId;
                roleToUpdate.RoleId = updatedRole.RoleId;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Exists(IdentityUserRole<int> userRole)
        {
            return _context.UserRoles.Where(r => r.Equals(userRole)).FirstOrDefault() != null;
        }
    }
}
