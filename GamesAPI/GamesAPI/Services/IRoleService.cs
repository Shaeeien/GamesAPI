using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GamesAPI.Services
{
    public interface IRoleService
    {
        public bool Add(IdentityRole<int> roleToAdd);
        public bool Remove(IdentityRole<int> roleToRemove);
        public bool Update(IdentityRole<int> roleToUpdate, IdentityRole<int> updatedRole);
        public IdentityRole<int>? FindByName(string name);
        public IdentityRole<int>? FindById(int id);
        public List<IdentityRole<int>> GetAll();
        public bool Exists(IdentityRole<int> role);
    }
}
