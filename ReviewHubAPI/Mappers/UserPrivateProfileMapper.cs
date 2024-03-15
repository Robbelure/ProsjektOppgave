using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers;

public class UserPrivateProfileMapper : IMapper<UserEntity, UserPrivateProfileDTO>
{
    public UserPrivateProfileDTO MapToDTO(UserEntity entity)
    {
        return new UserPrivateProfileDTO
        {
            UserID = entity.UserID,
            Username = entity.Username,
            Email = entity.Email,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            DateCreated = entity.DateCreated,
            IsAdmin = entity.IsAdmin
        };
    }

    public UserEntity MapToEntity(UserPrivateProfileDTO dto)
    {
        throw new NotImplementedException("Mapping from DTO to Entity is not required for this scenario.");
    }
}
