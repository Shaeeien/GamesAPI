using Microsoft.AspNetCore.Identity;

namespace GamesAPI.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime? TokenCreatedAt { get; set; } = null;
        public DateTime? Expires { get; set; } = null;
    }
}
