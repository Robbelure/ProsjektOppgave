using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
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


    /// <summary>
    /// Laster opp eller oppdaterer et profilbilde for en spesifikk bruker.
    /// </summary>
    /// <param name="userId">Brukerens ID for hvem bildet skal lastes opp.</param>
    /// <param name="file">Filobjektet mottatt fra frontend som inneholder bildet.</param>
    /// <returns>Melding om operasjonen var vellykket eller ikke.</returns>
    [HttpPost("Id={userId}")]
    public async Task<ActionResult<string>> AddProfilePicture([FromRoute] int userId, [FromForm] IFormFile file)
    {
        var message = await _profilePictureService.AddOrUpdateProfilePictureAsync(userId, file);
        if (!string.IsNullOrEmpty(message))
        {
            _logger.LogInformation("Profile picture uploaded successfully for user ID: {UserId}", userId);
            return Ok(message);
        }
        _logger.LogError("Failed to upload profile picture for user ID: {UserId}", userId);
        return BadRequest("Failed to upload profile picture");
    }


    [HttpGet(Name = "GetAllProfilePictures")]
    public async Task<ActionResult<ICollection<ProfilePictureDTO>>> GetAllProfilePictures(int pageSize, int pageNumber)
    {
        _logger.LogInformation("Received request to get all profile pictures with page size {PageSize} and page number {PageNumber}", pageSize, pageNumber);

        var allProfilePictures = await _profilePictureService.GetAllProfilePicturesAsync(pageSize, pageNumber);

        if (!allProfilePictures.Any())
        {
            _logger.LogInformation("No profile pictures were found");
            return NotFound("There are no profile pictures on the server");
        }

        _logger.LogInformation("Returning {Count} profile pictures", allProfilePictures.Count);
        return Ok(allProfilePictures);
    }


    [HttpGet("Id={UserId}", Name = "GetProfilePictureByUserId")]
    public async Task<ActionResult<ProfilePictureDTO>> GetProfilePictureByUserId(int UserId)
    {
        _logger.LogInformation("Received request to get profile picture for user ID: {UserId}", UserId);
        var profilePicture = await _profilePictureService.GetProfilePictureByUserIdAsync(UserId);

        if (profilePicture == null)
        {
            _logger.LogWarning("No profile picture found for user ID: {UserId}", UserId);
            return NotFound($"A profile picture with that userId {UserId} was not found.");
        }

        _logger.LogInformation("Returning profile picture for user ID: {UserId}", UserId);
        return Ok(profilePicture);
    }


    [HttpDelete("Id={UserId}", Name = "DeleteProfilePictureByUserId")]
    public async Task<ActionResult> DeleteProfilePictureByUserId(int UserId)
    {
        _logger.LogInformation("Attempting to delete profile picture for user ID: {UserId}", UserId);
        bool result = await _profilePictureService.DeleteProfilePictureByUserIdAsync(UserId);
        if (result)
        {
            _logger.LogInformation("Profile picture deleted successfully for user ID: {UserId}", UserId);
            return Ok("Profile picture deleted successfully.");
        }
        else
        {
            _logger.LogWarning("No profile picture found to delete for user ID: {UserId}", UserId);
            return NotFound("No profile picture found to delete.");
        }
    }
}
