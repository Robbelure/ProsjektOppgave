using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Services.Interface;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    Task<UserDTO?> GetUserByIdAsync(int userId);
    Task<UserDTO?> GetUserByUsernameAsync(string username);
    Task DeleteUserAsync(int userId);
}
