using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers;

public class UserPublicProfileMapper : IMapper<UserEntity, UserPublicProfileDTO>
{
    public UserPublicProfileDTO MapToDTO(UserEntity entity)
    {
        return new UserPublicProfileDTO
        {
            Username = entity.Username,
            ProfilePicture = entity.ProfilePicture,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname
        };
    }

    public UserEntity MapToEntity(UserPublicProfileDTO dto)
    {
        throw new NotImplementedException("Mapping from DTO to Entity is not required for this scenario.");
    }
}
