using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Middleware;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper<UserEntity, UserDTO> _userMapper;

    public UserService(IUserRepository userRepository, 
        IMapper<UserEntity, UserDTO> userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;

    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return users.Select(user => _userMapper.MapToDTO(user)).ToList();
    }

    public async Task<UserDTO?> GetUserByIdAsync(int userId)
    {
        var userEntity = await _userRepository.GetUserByIdAsync(userId);
        return userEntity == null ? null : _userMapper.MapToDTO(userEntity);
    }

    public async Task<UserDTO?> GetUserByUsernameAsync(string username)
    {
        var userEntity = await _userRepository.GetUserByUsernameAsync(username);
        if (userEntity == null)
        {
            return null;
        }

        return _userMapper.MapToDTO(userEntity);
    }

    public async Task DeleteUserAsync(int userId)
    {
        await _userRepository.DeleteUserAsync(userId);
    }
}
