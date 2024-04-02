using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface
{
    public interface IUploadProfilePictureRepository
    {
        public Task<ICollection<ProfilePictureEntity>> GetAllProfilePicturesAsync(int PageSize, int PageNummer);

        public Task<ProfilePictureEntity> GetProfilePictureByUserIdAsync(int UserId);

        public Task<ProfilePictureEntity> DeleteProfilePictureByUserIdAsync(int UserId);
    }
}
