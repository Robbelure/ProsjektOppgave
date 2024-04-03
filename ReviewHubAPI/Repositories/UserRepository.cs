using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Data;
using ReviewHubAPI.Middleware;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ReviewHubDbContext _dbContext;

    public UserRepository(ReviewHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task RegisterUserAsync(UserEntity user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<UserEntity?> GetUserByIdAsync(int userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        if (user == null)
        {
            throw new NotFoundException($"User with ID {userId} not found");
        }
        return user;
    }

    public async Task<UserEntity?> GetUserByUsernameAsync(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task UpdateUserAsync(UserEntity user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var userToDelete = await _dbContext.Users.FindAsync(userId);
        if (userToDelete != null)
        {
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _dbContext.Users.AnyAsync(user => user.Username == username);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email);
    }
}
