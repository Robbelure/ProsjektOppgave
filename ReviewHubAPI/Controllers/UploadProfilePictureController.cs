using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Services;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UploadProfilePictureController : Controller
{
    private readonly IUploadProfilePictureService _profilePictureService;
    private readonly ILogger<UploadProfilePictureController> _logger;

    public UploadProfilePictureController(IUploadProfilePictureService porfilePictureService, ILogger<UploadProfilePictureController> logger)
    {
        _profilePictureService = porfilePictureService;
        _logger = logger;
    }

    [HttpPost("Id={userId}")]
    public async Task<ActionResult<string>> AddProfilePicture([FromRoute] int userId, [FromForm] IFormFile file)
    {
        var message = await _profilePictureService.AddOrUpdateProfilePictureAsync(userId, file);
        _logger.LogInformation("Profile picture uploaded successfully for user ID: {UserId}", userId);
        return Ok(message);
    }

    [HttpGet(Name = "GetAllProfilePictures")]
    public async Task<ActionResult<ICollection<ProfilePictureDTO>>> GetAllProfilePictures(int PageSize, int PageNummer)
    {
        var alleprofilepictures = await _profilePictureService.GetAllProfilePicturesAsync(PageSize, PageNummer);

        if (alleprofilepictures == null)
            return NotFound("There are no profilepictures on the server");
        return Ok(alleprofilepictures);
    }

    [HttpGet ("Id={UserId}", Name = "GetProfilePictureByUserId")]
    public async Task<ActionResult<ProfilePictureDTO>> GetProfilePictureByUserId(int UserId)
    {
        var profilepicture = await _profilePictureService.GetProfilePictureByUserIdAsync(UserId);

        if (profilepicture == null)
            return NotFound($"A profilepicture with that userId {UserId}");
        return Ok(profilepicture);
    }

    [HttpDelete ("Id={UserId}",Name = "DeleteProfilePictureByUserId")]
    public async Task<ActionResult<ProfilePictureDTO>> DeleteProfilePictureByUserId(int UserId)
    {
        var profilePictureToDelete = await _profilePictureService.DeleteProfilePictureByUserIdAsync(UserId);

        if (profilePictureToDelete == null)
            return BadRequest("Profile picture could not be deleted");
        return Ok(profilePictureToDelete);
    }
}
