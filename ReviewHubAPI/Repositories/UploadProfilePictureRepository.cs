using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class UploadProfilePictureRepository : IUploadProfilePictureRepository
    {
        public Task<ProfilePictureEntity> DeleteProfilePictureByUserIdAsync(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ProfilePictureEntity>> GetAllProfilePicturesAsync(int PageSize, int PageNummer)
        {
            throw new NotImplementedException();
        }

        public Task<ProfilePictureEntity> GetProfilePictureByUserIdAsync(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}
