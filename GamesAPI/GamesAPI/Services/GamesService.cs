using GamesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        public GamesService(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public async Task<bool> Add(Game gameToAdd)
        {
            if(await Exists(gameToAdd) == false)
            {
                try
                {
                    _context.Games.Add(gameToAdd);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> Exists(Game game)
        {
            return await _context.Games.Where(g => g.Name == game.Name).FirstOrDefaultAsync() != null;
        }

        public async Task<Game?> FindById(int id)
        {
            return await _context.Games.Where(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Game?> FindByName(string name)
        {
            return await _context.Games.Where(g => g.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Game>> GetAll()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<bool> Remove(Game gameToRemove)
        {
            try
            {
                var removedGame = _context.Games.Remove(gameToRemove);
                if (removedGame != null)
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(Game gameToUpdate, Game updatedGame)
        {
            try
            {
                gameToUpdate.Reviews = updatedGame.Reviews;
                gameToUpdate.Description = updatedGame.Description;
                gameToUpdate.Name = updatedGame.Name;
                gameToUpdate.AvgRating = updatedGame.AvgRating;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
