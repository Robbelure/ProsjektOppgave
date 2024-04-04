using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers;

public class UserPublicProfileMapper : IMapper<User, UserPublicProfileDTO>
{
    public UserPublicProfileDTO MapToDTO(User entity)
    {
        return new UserPublicProfileDTO
        {
            Username = entity.Username,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname
        };
    }

    public User MapToEntity(UserPublicProfileDTO dto)
    {
        throw new NotImplementedException("Mapping from DTO to Entity is not required for this scenario.");
    }
}
