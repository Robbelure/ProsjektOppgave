using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Extensions;
using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, 
        ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        if(!users.Any())
            return NotFound("No users found.");
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{userId:int}")]
    public async Task<ActionResult<UserDTO>> GetUserById(int userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpGet("username/{username}")]
    public async Task<ActionResult<UserPublicProfileDTO>> GetUserByUsername(string username)
    {
        var user = await _userService.GetUserPublicProfileByUsernameAsync(username);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpGet("public/{userId:int}")]
    public async Task<ActionResult<UserPublicProfileDTO>> GetUserPublicProfileById(int userId)
    {
        var userPublicProfile = await _userService.GetUserPublicProfileByIdAsync(userId);
        if (userPublicProfile == null)
            return NotFound($"User with ID {userId} not found.");

        return Ok(userPublicProfile);
    }

    [Authorize]
    [HttpPut("{userId:int}")]
    public async Task<ActionResult> UpdateUser(int userId, [FromBody] UserUpdateDTO updateDto)
    {
        _logger.LogInformation($"Starting update process for user with ID {userId}.");

        var currentUserId = User.GetUserId(); // Antar at denne metoden henter brukerens ID fra token
        var isAdmin = User.IsInRole("Admin");

        if (currentUserId != userId && !isAdmin)
        {
            _logger.LogWarning($"User {currentUserId} does not have permission to update user with ID {userId}.");
            return Forbid();
        }

        var updateResult = await _userService.UpdateUserAsync(userId, updateDto, isAdmin);
        if (updateResult == null)
        {
            _logger.LogWarning($"User with ID {userId} update failed or no changes were made.");
            return BadRequest("Update failed or no changes were made.");
        }

        _logger.LogInformation($"User with ID {userId} updated successfully.");
        return Ok(updateResult);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var currentUserId = User.GetUserId();  // Henter brukerens ID basert på token
        var isAdmin = User.IsInRole("Admin");

        if (currentUserId != id && !isAdmin)
        {
            _logger.LogWarning($"User {currentUserId} attempted to delete user {id} without sufficient permissions.");
            return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to delete other users.");
        }

        await _userService.DeleteUserAsync(id);
        _logger.LogInformation($"User with ID {id} deleted successfully.");
        return Ok($"User with ID {id} was deleted successfully.");
    }
}
