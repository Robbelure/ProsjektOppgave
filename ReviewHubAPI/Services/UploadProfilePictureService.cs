using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Services
{
    public class UploadProfilePictureService : IUploadProfilePictureService
    {
        private readonly IUploadProfilePictureRepository _uploadprofilepicture;
        private readonly IMapper<ProfilePicture, ProfilePictureDTO> _profilepicturemapper;

        public UploadProfilePictureService( IUploadProfilePictureRepository uploadProfilePicture,IMapper<ProfilePicture, ProfilePictureDTO> profilepicturemapper)
        {
            _uploadprofilepicture = uploadProfilePicture;
            _profilepicturemapper = profilepicturemapper;
        }

        public async Task<string> AddOrUpdateProfilePictureAsync(int userId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty");
            }

            var pictureBytes = await GetPictureBytesAsync(file); // Hjelpemetode for å konvertere IFormFile til byte-array.

            ProfilePicture entity = await _uploadprofilepicture.GetProfilePictureByUserIdAsync(userId) ?? new ProfilePicture { UserId = userId };
            entity.Picture = pictureBytes;

            var result = await _uploadprofilepicture.AddOrUpdateProfilePictureAsync(entity);
            return result;
        }

        public async Task<ProfilePictureDTO> DeleteProfilePictureByUserIdAsync(int UserId)
        {
            var profilepic = await _uploadprofilepicture.DeleteProfilePictureByUserIdAsync(UserId);

            return _profilepicturemapper.MapToDTO(profilepic) ?? null!;
        }

        public async Task<ICollection<ProfilePictureDTO>> GetAllProfilePicturesAsync(int PageSize, int PageNummer)
        {
            var allpics = await _uploadprofilepicture.GetAllProfilePicturesAsync(PageSize, PageNummer);
            List<ProfilePictureDTO> returnpics = new ();

            if (allpics != null)
            {
                return null!;
            }

            foreach (var picture in allpics!)
            {
                returnpics.Add(_profilepicturemapper.MapToDTO(picture));
            }

            return returnpics;
        }

        public async Task<ProfilePictureDTO> GetProfilePictureByUserIdAsync(int UserId)
        {
            var picture = await _uploadprofilepicture.GetProfilePictureByUserIdAsync(UserId);

            return _profilepicturemapper.MapToDTO(picture) ?? null!;    
        }

        private async Task<byte[]> GetPictureBytesAsync(IFormFile picture)
        {

            using (var memoryStream = new System.IO.MemoryStream())
            {
                await picture.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
