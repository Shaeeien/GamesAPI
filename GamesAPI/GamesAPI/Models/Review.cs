using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GamesAPI.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ReviewContent { get; set; }
        [Required]
        public float Rating { get; set; }
        public Game Game { get; set; }
        public int GameId { get; set; }
        public AppUser User { get; set; }
        public int UserId { get; set; }

    }
}
