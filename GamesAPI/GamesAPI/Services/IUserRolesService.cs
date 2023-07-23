using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GamesAPI.Services
{
    public interface IUserRolesService
    {
        public bool Add(IdentityUserRole<int> roleToAdd);
        public bool Remove(IdentityUserRole<int> roleToRemove);
        public bool Update(IdentityUserRole<int> roleToUpdate, IdentityUserRole<int> updatedRole);
        public List<IdentityUserRole<int>>? FindByUserId(int id);
        public List<IdentityUserRole<int>>? FindByRoleId(int id);
        public List<IdentityUserRole<int>> GetAll();
        public bool Exists(IdentityUserRole<int> userRole);
    }
}
