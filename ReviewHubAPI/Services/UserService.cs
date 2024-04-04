using Microsoft.EntityFrameworkCore;
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
        return users.Select(user => _userMapper.MapToDTO(user)).ToList();
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

    public async Task DeleteUserAsync(int userId)
    {
        var userEntity = await _userRepository.GetUserByIdAsync(userId);
        if (userEntity == null)
        {
            _logger.LogError($"User with ID {userId} not found.");
            throw new NotFoundException($"User with ID {userId} not found.");
        }

        await _userRepository.DeleteUserAsync(userId);
        _logger.LogInformation($"User with ID {userId} has been successfully deleted.");
    }

    public async Task<UserDTO?> UpdateUserAsync(int userId, UserUpdateDTO userUpdateDto)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            _logger.LogError($"User with ID {userId} not found for update.");
            return null;
        }

        bool isUpdated = false;

        if (!string.IsNullOrWhiteSpace(userUpdateDto.Username) && userUpdateDto.Username != user.Username)
        {
            if (await _userRepository.UsernameExistsAsync(userUpdateDto.Username))
            {
                _logger.LogWarning($"Username {userUpdateDto.Username} already exists.");
                return null;
            }
            user.Username = userUpdateDto.Username;
            isUpdated = true;
        }

        if (!string.IsNullOrWhiteSpace(userUpdateDto.Email) && userUpdateDto.Email != user.Email)
        {
            if (await _userRepository.EmailExistsAsync(userUpdateDto.Email))
            {
                _logger.LogWarning($"Email {userUpdateDto.Email} already in use.");
                return null;
            }
            user.Email = userUpdateDto.Email;
            isUpdated = true;
        }

        if (!string.IsNullOrWhiteSpace(userUpdateDto.Firstname) && userUpdateDto.Firstname != user.Firstname)
        {
            user.Firstname = userUpdateDto.Firstname;
            isUpdated = true;
        }

        if (!string.IsNullOrWhiteSpace(userUpdateDto.Lastname) && userUpdateDto.Lastname != user.Lastname)
        {
            user.Lastname = userUpdateDto.Lastname;
            isUpdated = true;
        }

        if (isUpdated)
        {
            _logger.LogInformation($"Updating user with ID {userId}.");
            await _userRepository.UpdateUserAsync(user);
            _logger.LogInformation($"User with ID {userId} updated in database.");
        }

        return _userMapper.MapToDTO(user);
    }

    public async Task<bool> IsUserAdminAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user?.IsAdmin ?? false;
    }

}

