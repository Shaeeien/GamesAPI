using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _authContext;
        public RoleService(ApplicationDbContext ctx) 
        { 
            _authContext = ctx;
        }

        public bool Add(IdentityRole<int> roleToAdd)
        {
            try
            {
                _authContext.Roles.Add(roleToAdd);
                _authContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool Exists(IdentityRole<int> role)
        {
            return _authContext.UserRoles.Where(r => r.Equals(role)).FirstOrDefault() != null;
        }

        public IdentityRole<int>? FindById(int id)
        {
            return _authContext.Roles.Where(r => r.Id == id).FirstOrDefault();
        }

        public IdentityRole<int>? FindByName(string name)
        {
            return _authContext.Roles.Where(r => r.Name == name).FirstOrDefault();
        }

        public List<IdentityRole<int>> GetAll()
        {
            return _authContext.Roles.ToList();
        }

        public bool Remove(IdentityRole<int> roleToRemove)
        {
            var removedRole = _authContext.Roles.Remove(roleToRemove);
            if(removedRole != null)
            {
                _authContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(IdentityRole<int> roleToUpdate, IdentityRole<int> updatedRole)
        {
            try
            {
                roleToUpdate.Name = updatedRole.Name;
                _authContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
