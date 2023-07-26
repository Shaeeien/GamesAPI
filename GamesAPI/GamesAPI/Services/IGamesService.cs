using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GamesAPI.Services
{
    public interface IGamesService
    {
        public Task<bool> Add(Game gameToAdd);
        public Task<bool> Remove(Game gameToRemove);
        public Task<bool> Update(Game gameToUpdate, Game updatedGame);
        public Task<Game?> FindByName(string name);
        public Task<Game?> FindById(int id);
        public Task<List<Game>> GetAll();
        public Task<bool> Exists(Game game);
        public Task<int> SaveChanges();
    }
}
