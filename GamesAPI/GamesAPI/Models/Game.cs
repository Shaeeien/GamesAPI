using System.ComponentModel.DataAnnotations;

namespace GamesAPI.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float AvgRating { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
