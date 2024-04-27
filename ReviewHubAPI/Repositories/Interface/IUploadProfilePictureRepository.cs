using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface;

public interface IUploadProfilePictureRepository
{
    public Task<string> AddOrUpdateProfilePictureAsync(ProfilePicture entity);
    public Task<ICollection<ProfilePicture>> GetAllProfilePicturesAsync(int PageSize, int PageNummer);
    public Task<ProfilePicture?> GetProfilePictureByUserIdAsync(int UserId);
    public Task<bool> DeleteProfilePictureAsync(ProfilePicture profilePicture);
}
