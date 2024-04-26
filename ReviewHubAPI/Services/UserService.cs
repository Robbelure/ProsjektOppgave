using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Extensions;
using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Middleware;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;
using System.Security.Claims;

namespace ReviewHubAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper<User, UserDTO> _userMapper;
    private readonly IMapper<User, UserPublicProfileDTO> _userPublicProfileMapper;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, 
        IMapper<User, UserDTO> userMapper, 
        IMapper<User, UserPublicProfileDTO> userPublicProfileMapper,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _userPublicProfileMapper = userPublicProfileMapper;
        _logger = logger;
    }

    public async Task<IEnumerable<UserDTO?>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return users.Select(_userMapper.MapToDTO).ToList();
    }

    public async Task<UserDTO?> GetUserByIdAsync(int userId)
    {
        var userEntity = await _userRepository.GetUserByIdAsync(userId);
        return userEntity == null ? null : _userMapper.MapToDTO(userEntity);
    }

    public async Task<UserPublicProfileDTO?> GetUserPublicProfileByUsernameAsync(string username)
    {
        var userEntity = await _userRepository.GetUserByUsernameAsync(username);
        return userEntity == null ? null : _userPublicProfileMapper.MapToDTO(userEntity);
    }

    public async Task<UserPublicProfileDTO?> GetUserPublicProfileByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user == null ? null : _userPublicProfileMapper.MapToDTO(user);
    }

    public async Task<UserDTO?> UpdateUserAsync(int userId, UserUpdateDTO userUpdateDto, bool isAdmin)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            _logger.LogError($"User with ID {userId} not found for update.");
            throw new InvalidOperationException($"User with ID {userId} not found.");
        }

        List<string> updatedFields = new List<string>();

        if (isAdmin)
        {
            if (!string.IsNullOrWhiteSpace(userUpdateDto.Username) && userUpdateDto.Username != user.Username)
            {
                user.Username = userUpdateDto.Username;
                updatedFields.Add($"Username='{userUpdateDto.Username}'");
            }

            if (!string.IsNullOrWhiteSpace(userUpdateDto.Email) && userUpdateDto.Email != user.Email)
            {
                user.Email = userUpdateDto.Email;
                updatedFields.Add($"Email='{userUpdateDto.Email}'");
            }
        }

        if (!string.IsNullOrWhiteSpace(userUpdateDto.Firstname) && userUpdateDto.Firstname != user.Firstname)
        {
            user.Firstname = userUpdateDto.Firstname;
            updatedFields.Add($"Firstname='{userUpdateDto.Firstname}'");
        }

        if (!string.IsNullOrWhiteSpace(userUpdateDto.Lastname) && userUpdateDto.Lastname != user.Lastname)
        {
            user.Lastname = userUpdateDto.Lastname;
            updatedFields.Add($"Lastname='{userUpdateDto.Lastname}'");
        }

        if (updatedFields.Any())
        {
            await _userRepository.UpdateUserAsync(user);
            _logger.LogInformation($"User {userId} updated successfully: {string.Join(", ", updatedFields)}.");
            return _userMapper.MapToDTO(user);
        }

        _logger.LogInformation($"No updates were made for user {userId}, no changes in input data.");
        return _userMapper.MapToDTO(user);
    }

    public async Task DeleteUserAsync(int userId)
    {
        var userToDelete = await _userRepository.GetUserByIdAsync(userId);
        if (userToDelete == null)
        {
            _logger.LogError($"User with ID {userId} not found.");
            throw new NotFoundException($"User with ID {userId} not found.");
        }

        await _userRepository.DeleteUserAsync(userId); 
        _logger.LogInformation($"User with ID {userId} has been successfully deleted.");
    }

    // overflødig?
    public async Task<bool> IsUserAdminAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user?.IsAdmin ?? false;
    }
}

