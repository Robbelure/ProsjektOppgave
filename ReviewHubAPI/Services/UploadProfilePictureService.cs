using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Services;

public class UploadProfilePictureService : IUploadProfilePictureService
{
    private readonly IUploadProfilePictureRepository _uploadProfilePictureRepository;
    private readonly IMapper<ProfilePicture, ProfilePictureDTO> _profilepicturemapper;
    private readonly ILogger _logger;

    public UploadProfilePictureService(IUploadProfilePictureRepository uploadProfilePicture,
        IMapper<ProfilePicture, ProfilePictureDTO> profilepicturemapper,
        ILogger logger)
    {
        _uploadProfilePictureRepository = uploadProfilePicture;
        _profilepicturemapper = profilepicturemapper;
        _logger = logger;
    }

    public async Task<string> AddOrUpdateProfilePictureAsync(int userId, IFormFile file)
    {
        byte[] pictureBytes;

        if (file == null || file.Length == 0)
        {
            // henter default profilbilde bytes asynkront
            pictureBytes = await GetDefaultProfilePictureBytesAsync();
        }
        else
        {
            // konverterer mottatt fil til bytes
            pictureBytes = await GetPictureBytesAsync(file);
        }

        var entity = await _uploadProfilePictureRepository.GetProfilePictureByUserIdAsync(userId) ?? new ProfilePicture { UserId = userId };
        entity.Picture = pictureBytes;

        var result = await _uploadProfilePictureRepository.AddOrUpdateProfilePictureAsync(entity);
        return result;
    }

    public async Task<ICollection<ProfilePictureDTO>> GetAllProfilePicturesAsync(int pageSize, int pageNumber)
    {
        _logger.LogInformation("Fetching all profile pictures from repository with page size {PageSize} and page number {PageNumber}", pageSize, pageNumber);

        var profilePictures = await _uploadProfilePictureRepository.GetAllProfilePicturesAsync(pageSize, pageNumber);
        var profilePictureDTOs = profilePictures.Select(_profilepicturemapper.MapToDTO).ToList();

        if (!profilePictureDTOs.Any())
            _logger.LogInformation("No profile pictures were found in repository");

        return profilePictureDTOs;
    }

    public async Task<ProfilePictureDTO?> GetProfilePictureByUserIdAsync(int UserId)
    {
        _logger.LogInformation("Attempting to fetch profile picture for user ID: {UserId}", UserId);
        var picture = await _uploadProfilePictureRepository.GetProfilePictureByUserIdAsync(UserId);

        if (picture == null)
        {
            _logger.LogWarning("Profile picture not found for user ID: {UserId}", UserId);
            return null;
        }

        _logger.LogInformation("Profile picture retrieved for user ID: {UserId}", UserId);
        return _profilepicturemapper.MapToDTO(picture);
    }

    public async Task<bool> DeleteProfilePictureByUserIdAsync(int userId)
    {
        var profilePicture = await _uploadProfilePictureRepository.GetProfilePictureByUserIdAsync(userId);
        if (profilePicture != null)
        {
            _logger.LogInformation("Profile picture found for user ID: {UserId}, attempting to delete", userId);
            bool deleted = await _uploadProfilePictureRepository.DeleteProfilePictureAsync(profilePicture);
            if (deleted)
            {
                _logger.LogInformation("Profile picture successfully deleted for user ID: {UserId}", userId);
                return true;
            }
            _logger.LogError("Failed to delete profile picture for user ID: {UserId}", userId);
        }
        else
        {
            _logger.LogWarning("No profile picture found for user ID: {UserId}", userId);
        }
        return false;
    }

    private async Task<byte[]> GetPictureBytesAsync(IFormFile picture)
    {
        using (var memoryStream = new System.IO.MemoryStream())
        {
            await picture.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

    private async Task<byte[]> GetDefaultProfilePictureBytesAsync()
    {
        var pathToDefaultImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profile-icon.jpg");
        return await File.ReadAllBytesAsync(pathToDefaultImage);
    }
}
