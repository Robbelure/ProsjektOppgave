using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Services.Interface;

public interface IUserService
{
    Task<UserDTO?> AddUserAsync(UserDTO newUser);
    Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    Task<UserDTO?> GetUserByIdAsync(int userId);
    Task DeleteUserAsync(int userId);
}
