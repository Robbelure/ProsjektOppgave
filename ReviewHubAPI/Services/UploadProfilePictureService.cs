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
            byte[] pictureBytes;

            if (file == null || file.Length == 0)
            {
                // Hent standard profilbilde bytes asynkront
                pictureBytes = await GetDefaultProfilePictureBytesAsync();
            }
            else
            {
                // Konverter mottatt fil til bytes
                pictureBytes = await GetPictureBytesAsync(file);
            }

            var entity = await _uploadprofilepicture.GetProfilePictureByUserIdAsync(userId) ?? new ProfilePicture { UserId = userId };
            entity.Picture = pictureBytes;

            var result = await _uploadprofilepicture.AddOrUpdateProfilePictureAsync(entity);
            return result;
        }
        public async Task<ProfilePictureDTO> DeleteProfilePictureByUserIdAsync(int UserId)
        {
            var profilepic = await _uploadprofilepicture.DeleteProfilePictureByUserIdAsync(UserId);

            return _profilepicturemapper.MapToDTO(profilepic) ?? null!;
        }
        public async Task<ICollection<ProfilePictureDTO>> GetAllProfilePicturesAsync(int pageSize, int pageNumber)
        {
            var profilePictures = await _uploadprofilepicture.GetAllProfilePicturesAsync(pageSize, pageNumber);

            // Sjekk om listen er tom og håndter dette tilfellet
            if (profilePictures.Count == 0)
                return new List<ProfilePictureDTO>();  // Returner en tom DTO liste

            // Mapper hver entitet til en DTO
            var profilePictureDTOs = profilePictures.Select(_profilepicturemapper.MapToDTO).ToList();
            return profilePictureDTOs;
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
        private async Task<byte[]> GetDefaultProfilePictureBytesAsync()
        {
            var pathToDefaultImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profile-icon.jpg");
            return await File.ReadAllBytesAsync(pathToDefaultImage);
        }
    }
}
