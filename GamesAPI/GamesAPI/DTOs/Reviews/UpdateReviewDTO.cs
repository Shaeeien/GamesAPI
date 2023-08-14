namespace GamesAPI.DTOs.Reviews
{
    public class UpdateReviewDTO
    {
        public float? Rating { get; set; }
        public string? ReviewContent { get; set; }
        public int? GameId { get; set; }
    }
}
