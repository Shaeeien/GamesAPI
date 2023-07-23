using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;


namespace GamesAPI.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-EII9684; Database=GameReviewerv2; Trusted_Connection=True; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().HasKey(u => u.Id);
            builder.Entity<AppUser>().Ignore(u => u.EmailConfirmed)
               .Ignore(u => u.AccessFailedCount)
               .Ignore(u => u.ConcurrencyStamp)
               .Ignore(u => u.LockoutEnabled)
               .Ignore(u => u.LockoutEnd)
               .Ignore(u => u.NormalizedEmail)
               .Ignore(u => u.PhoneNumber)
               .Ignore(u => u.PhoneNumberConfirmed)
               .Ignore(u => u.SecurityStamp)
               .Ignore(u => u.TwoFactorEnabled)
               .Ignore(u => u.NormalizedUserName);
            builder.Entity<Game>().HasKey(g => g.Id);
            builder.Entity<Review>().HasKey(r => r.Id);
            builder.Entity<Game>().HasMany(g => g.Reviews).WithOne(r => r.Game).HasForeignKey(g => g.GameId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Review>().HasOne(r => r.Game).WithMany(g => g.Reviews).HasForeignKey(r => r.GameId).OnDelete(DeleteBehavior.Cascade);
          
            builder.Entity<IdentityRole<int>>().HasKey(r => r.Id);
            builder.Entity<IdentityRole<int>>().Ignore(r => r.NormalizedName)
                .Ignore(r => r.ConcurrencyStamp);             
                
            builder.Entity<IdentityRole<int>>().HasData(
                new { Id = 1, Name = "User" },
                new { Id = 2, Name = "Admin" }
            );

            builder.Entity<AppUser>().HasData(
                new AppUser { Id = 1, UserName = "Admin", Email = "admin@games-api.pl", PasswordHash = BC.HashPassword("Admin2023!") }
            );

            builder.Entity<IdentityUserRole<int>>().HasData(
                new { UserId = 1, RoleId = 2 }
            );
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
