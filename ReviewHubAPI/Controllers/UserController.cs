using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Extensions;
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

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var currentId = User.GetUserId();
        var isAdmin = await _userService.IsUserAdminAsync(currentId);

        if (!isAdmin && currentId != id)
        {
            _logger.LogWarning($"User with ID {currentId} does not have permission to delete user with ID {id}.");
            return StatusCode(StatusCodes.Status403Forbidden, new { Message = "You do not have permission to delete other users." });
        }

        await _userService.DeleteUserAsync(id);
        return Ok($"User with ID {id} deleted successfully.");
    }
}
