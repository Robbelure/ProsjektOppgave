using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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

    public async Task<UserDTO> UpdateUserAsync(int userId, UserUpdateDTO userUpdateDto, ClaimsPrincipal currentUser)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
            throw new InvalidOperationException($"User with ID {userId} not found.");

        // Kun fornavn og etternavn kan endres, og kun admin kan endre andre felter
        if (currentUser.IsInRole("Admin"))
        {
            if (!string.IsNullOrWhiteSpace(userUpdateDto.Username))
                user.Username = userUpdateDto.Username;

            if (!string.IsNullOrWhiteSpace(userUpdateDto.Email))
                user.Email = userUpdateDto.Email;
        }

        if (!string.IsNullOrWhiteSpace(userUpdateDto.Firstname))
            user.Firstname = userUpdateDto.Firstname;

        if (!string.IsNullOrWhiteSpace(userUpdateDto.Lastname))
            user.Lastname = userUpdateDto.Lastname;

        await _userRepository.UpdateUserAsync(user);
        return _userMapper.MapToDTO(user);
    }

    public async Task<bool> IsUserAdminAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user?.IsAdmin ?? false;
    }

    // trenger vi patch?
    /*
    public async Task<UserDTO?> PatchUserAsync(int userId, JsonPatchDocument<UserUpdateDTO> patchDoc)
    {
        _logger.LogInformation($"Starting patch process for user with ID {userId}.");

        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning($"User with ID {userId} not found for patch operation.");
            return null; // NotFound håndteres av GlobalExceptionMiddleware
        }

        var userDto = _userMapper.MapToDTO(user);
        var patchedDto = new UserUpdateDTO();

        patchDoc.ApplyTo(patchedDto); // Dette vil kaste en exception hvis det feiler, som middleware vil fange opp.

        // Manuell mapping fra patchedDto til user
        MapUpdateDtoToEntity(patchedDto, user);

        await _userRepository.UpdateUserAsync(user); // Enhver exception her vil også bli fanget av middleware.

        _logger.LogInformation($"Patch process for user with ID {userId} completed successfully.");

        return _userMapper.MapToDTO(user); // Returnerer den oppdaterte UserDTO.
    }
    

    private void MapUpdateDtoToEntity(UserUpdateDTO dto, User user)
    {
        if (!string.IsNullOrWhiteSpace(dto.Username))
            user.Username = dto.Username;
        if (!string.IsNullOrWhiteSpace(dto.Email))
            user.Email = dto.Email;
        if (!string.IsNullOrWhiteSpace(dto.Firstname))
            user.Firstname = dto.Firstname;
        if (!string.IsNullOrWhiteSpace(dto.Lastname))
            user.Lastname = dto.Lastname;
    }
    */
}

