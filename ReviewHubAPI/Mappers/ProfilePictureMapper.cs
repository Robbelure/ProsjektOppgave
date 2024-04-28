using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers;

public class ProfilePictureMapper : IMapper<ProfilePicture, ProfilePictureDTO>
{
    public ProfilePictureDTO MapToDTO(ProfilePicture entity)
    {
        if (entity == null)
            return null;

        return new ProfilePictureDTO
        {
            Id = entity.Id,
            UserId = entity.UserId,
            ProfilePicture = entity.Picture,
            DateCreated = entity.DateCreated,
            DateUpdated = entity.DateUpdated
        };
    }

    public ProfilePicture MapToEntity(ProfilePictureDTO dto)
    {
        if (dto == null)        
            return null;
        
        return new ProfilePicture
        {
            Id = dto.Id,
            UserId = dto.UserId,
            Picture = dto.ProfilePicture,
            DateCreated = dto.DateCreated,
            DateUpdated = dto.DateUpdated

        };
    }
}
