using GamesAPI.Models;

namespace GamesAPI.DTOs.Games
{
    public class CreateGameDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float AvgRating { get; set; }
    }
}
