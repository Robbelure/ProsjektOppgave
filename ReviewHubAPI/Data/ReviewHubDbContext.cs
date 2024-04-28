using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Utilities;
using System.Reflection.Metadata;

namespace ReviewHubAPI.Data;

public class ReviewHubDbContext : DbContext
{
    public ReviewHubDbContext(DbContextOptions<ReviewHubDbContext> options)
    : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<MoviePoster> MoviePoster { get; set; }
    public DbSet<ProfilePicture> ProfilePicture { get; set; }
    public DbSet<ReviewPicture> ReviewPicture { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
   
        base.OnModelCreating(modelBuilder);

        // Seeding av admin-bruker
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = -1,
            Username = "TheOne",
            PasswordHash = PasswordHelper.HashPassword("AdminPassword123!"),
            Email = "admin@proton.me",
            Firstname = "Admin",
            Lastname = "User",
            DateCreated = DateTime.UtcNow,
            IsAdmin = true
        });   
    }
}
