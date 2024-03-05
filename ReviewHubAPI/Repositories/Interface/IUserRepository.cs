using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface;

public interface IUserRepository
{
    Task AddUserAsync(UserEntity user);
    Task<IEnumerable<UserEntity>> GetAllUsersAsync();
    Task<UserEntity> GetUserByIdAsync(int userId);
    Task DeleteUserAsync(int userId);
}
