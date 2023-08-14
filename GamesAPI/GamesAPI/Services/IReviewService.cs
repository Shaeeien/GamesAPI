using GamesAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GamesAPI.Services
{
    public interface IReviewService
    {
        public Task<bool> Add(Review reviewToAdd);
        public Task<bool> Remove(Review reviewToRemove);
        public Task<bool> Update(Review reviewToUpdate, Review updatedReview);
        public Task<List<Review>?> FindByUser(int userId);
        public Task<Review?> FindById(int id);
        public Task<List<Review>> GetAll();
        public Task<bool> Exists(Review review);
        public Task<int> SaveChanges();
    }
}
