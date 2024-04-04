using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
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

        public async Task<string> AddNewProfilePicture(IFormFile file, int UserId)
        {
            var userpic = await GetPictureBytesAsync(file);
            ProfilePicture entity = new ProfilePicture
            {
                UserId = UserId,
                Picture = userpic
            };
            var message = await _uploadprofilepicture.AddProfilePicture(entity);

            return message;
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
