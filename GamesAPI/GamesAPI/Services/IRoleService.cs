using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GamesAPI.Services
{
    public interface IRoleService
    {
        public Task<bool> Add(IdentityRole<int> roleToAdd);
        public Task<bool> Remove(IdentityRole<int> roleToRemove);
        public Task<bool> Update(IdentityRole<int> roleToUpdate, IdentityRole<int> updatedRole);
        public Task<IdentityRole<int>?> FindByName(string name);
        public Task<IdentityRole<int>?> FindById(int id);
        public List<IdentityRole<int>> GetAll();
        public bool Exists(IdentityRole<int> role);
    }
}
