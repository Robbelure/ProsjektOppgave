using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Data;

public class ReviewHubDbContext : DbContext
{
    public ReviewHubDbContext(DbContextOptions<ReviewHubDbContext> options)
    : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
}
