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
    private readonly IMapper<UserEntity, UserPublicProfileDTO> _userPublicProfileMapper;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, 
        IMapper<UserEntity, UserDTO> userMapper, 
        IMapper<UserEntity, UserPublicProfileDTO> userPublicProfileMapper,
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

    public async Task<bool> IsUserAdminAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user?.IsAdmin ?? false;
    }

}

