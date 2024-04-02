using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers
{
    public class ProfilePictureMapper : IMapper<ProfilePictureEntity, ProfilePictureDTO>
    {
        public ProfilePictureDTO MapToDTO(ProfilePictureEntity entity)
        {
            return new ProfilePictureDTO
            {
                Id = entity.Id,
                UserID = entity.UserID,
                ProfilePicture = entity.ProfilePicture

            };
        }

        public ProfilePictureEntity MapToEntity(ProfilePictureDTO dto)
        {
            return new ProfilePictureEntity
            {
                Id = dto.Id,
                UserID = dto.UserID,
                ProfilePicture = dto.ProfilePicture
            };
        }
    }
}
