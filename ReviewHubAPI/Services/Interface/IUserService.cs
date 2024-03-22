using ReviewHubAPI.Models.DTO;

namespace ReviewHubAPI.Services.Interface;

public interface IUserService
{
    Task<IEnumerable<UserDTO?>> GetAllUsersAsync();
    Task<UserDTO?> GetUserByIdAsync(int userId);
    Task<UserPublicProfileDTO?> GetUserPublicProfileByUsernameAsync(string username);
    Task DeleteUserAsync(int userId);
    Task<bool> IsUserAdminAsync(int userId);
}
