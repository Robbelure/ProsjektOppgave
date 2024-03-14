using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface;

public interface IUserRepository
{
    Task RegisterUserAsync(UserEntity user);
    Task<IEnumerable<UserEntity>> GetAllUsersAsync();
    Task<UserEntity> GetUserByIdAsync(int userId);
    Task<UserEntity?> GetUserByUsernameAsync(string username);
    Task DeleteUserAsync(int userId);

    Task<bool> UsernameExistsAsync(string username);
    Task<bool> EmailExistsAsync(string email);
}
