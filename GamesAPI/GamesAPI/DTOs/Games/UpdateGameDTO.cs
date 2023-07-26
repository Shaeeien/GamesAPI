using GamesAPI.Models;

namespace GamesAPI.DTOs.Games
{
    public class UpdateGameDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float? AvgRating { get; set; }
        public List<Review>? Reviews { get; set; }
    }
}
