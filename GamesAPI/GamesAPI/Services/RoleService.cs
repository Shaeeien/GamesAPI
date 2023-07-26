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

        public async Task<bool> Add(IdentityRole<int> roleToAdd)
        {
            try
            {
                await _authContext.Roles.AddAsync(roleToAdd);
                await _authContext.SaveChangesAsync();
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

        public async Task<IdentityRole<int>?> FindById(int id)
        {
            return await _authContext.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IdentityRole<int>?> FindByName(string name)
        {
            return await _authContext.Roles.Where(r => r.Name == name).FirstOrDefaultAsync();
        }

        public List<IdentityRole<int>> GetAll()
        {
            return _authContext.Roles.ToList();
        }

        public async Task<bool> Remove(IdentityRole<int> roleToRemove)
        {
            var removedRole = _authContext.Roles.Remove(roleToRemove);
            if(removedRole != null)
            {
                await _authContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Update(IdentityRole<int> roleToUpdate, IdentityRole<int> updatedRole)
        {
            try
            {
                roleToUpdate.Name = updatedRole.Name;
                await _authContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
