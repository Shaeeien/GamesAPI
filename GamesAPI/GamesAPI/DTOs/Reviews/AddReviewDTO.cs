using GamesAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace GamesAPI.DTOs.Reviews
{
    public class AddReviewDTO
    {
        public string ReviewContent { get; set; }
        public float Rating { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
    }
}
