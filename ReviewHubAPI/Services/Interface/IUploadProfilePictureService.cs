using ReviewHubAPI.Models.DTO;

namespace ReviewHubAPI.Services.Interface;
public interface IUploadProfilePictureService
{
    public Task<string> AddOrUpdateProfilePictureAsync(int userId, IFormFile file);
    public Task<ICollection<ProfilePictureDTO>> GetAllProfilePicturesAsync(int PageSize, int PageNummer);
    public Task<ProfilePictureDTO?> GetProfilePictureByUserIdAsync(int UserId);
    public Task<bool> DeleteProfilePictureByUserIdAsync(int userId);
}
