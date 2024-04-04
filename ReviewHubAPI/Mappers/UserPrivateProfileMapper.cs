using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers;

public class UserPrivateProfileMapper : IMapper<User, UserPrivateProfileDTO>
{
    public UserPrivateProfileDTO MapToDTO(User entity)
    {
        return new UserPrivateProfileDTO
        {
            Id = entity.Id,
            Username = entity.Username,
            Email = entity.Email,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            DateCreated = entity.DateCreated,
            IsAdmin = entity.IsAdmin
        };
    }

    public User MapToEntity(UserPrivateProfileDTO dto)
    {
        throw new NotImplementedException("Mapping from DTO to Entity is not required for this scenario.");
    }
}
