using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Services.Interface
{
    public interface IUploadProfilePictureService
    {
        public Task<ICollection<ProfilePictureDTO>> GetAllProfilePicturesAsync(int PageSize, int PageNummer);

        public Task<ProfilePictureDTO> GetProfilePictureByUserIdAsync(int UserId);

        public Task<ProfilePictureDTO> DeleteProfilePictureByUserIdAsync(int UserId);

    }
}
