using GamesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Review reviewToAdd)
        {
            try
            {
                await _context.Reviews.AddAsync(reviewToAdd);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Exists(Review review)
        {
            return await _context.Reviews.Where(r => r.UserId == review.UserId && r.GameId == review.GameId).FirstOrDefaultAsync() != null;
        }

        public async Task<Review?> FindById(int id)
        {
            return await _context.Reviews.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Review>?> FindByUser(int userId)
        {
            return await _context.Reviews.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<List<Review>> GetAll()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<bool> Remove(Review reviewToRemove)
        {
            try
            {
                var removedReview = _context.Reviews.Remove(reviewToRemove);
                if(removedReview != null)
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

        public async Task<bool> Update(Review reviewToUpdate, Review updatedReview)
        {
            try
            {
                reviewToUpdate.ReviewContent = updatedReview.ReviewContent;
                reviewToUpdate.User = updatedReview.User;
                reviewToUpdate.UserId = updatedReview.UserId;
                reviewToUpdate.Game = updatedReview.Game;
                reviewToUpdate.GameId = updatedReview.GameId;
                await _context.SaveChangesAsync();
                return true;
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
    }
}
