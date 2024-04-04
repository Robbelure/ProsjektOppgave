using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface
{
    public interface IUploadProfilePictureRepository
    {
        public Task<String> AddProfilePicture(ProfilePicture entity);
        public Task<ICollection<ProfilePicture>> GetAllProfilePicturesAsync(int PageSize, int PageNummer);

        public Task<ProfilePicture> GetProfilePictureByUserIdAsync(int UserId);

        public Task<ProfilePicture> DeleteProfilePictureByUserIdAsync(int UserId);
    }
}
