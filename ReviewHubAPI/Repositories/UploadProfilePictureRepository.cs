using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class UploadProfilePictureRepository : IUploadProfilePictureRepository
    {
        public Task<string> AddProfilePicture(ProfilePicture entity)
        {
            throw new NotImplementedException();
        }

        public Task<ProfilePicture> DeleteProfilePictureByUserIdAsync(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ProfilePicture>> GetAllProfilePicturesAsync(int PageSize, int PageNummer)
        {
            throw new NotImplementedException();
        }

        public Task<ProfilePicture> GetProfilePictureByUserIdAsync(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}
