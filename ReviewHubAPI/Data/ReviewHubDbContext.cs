using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Utilities;

namespace ReviewHubAPI.Data;

public class ReviewHubDbContext : DbContext
{
    public ReviewHubDbContext(DbContextOptions<ReviewHubDbContext> options)
    : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seeding av admin-bruker
        modelBuilder.Entity<UserEntity>().HasData(new UserEntity
        {
            UserID = -1,
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
