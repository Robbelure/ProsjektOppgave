using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers;

public class UserMapper : IMapper<UserEntity, UserDTO>
{
    public UserDTO MapToDTO(UserEntity entity)
    {
        return new UserDTO
        {
            UserID = entity.UserID,
            Username = entity.Username,
            ProfilePicture = entity.ProfilePicture,
            Email = entity.Email,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            DateCreated = entity.DateCreated
        };
    }

    public UserEntity MapToEntity(UserDTO dto)
    {
        return new UserEntity
        {
            UserID = dto.UserID,
            Username = dto.Username,
            ProfilePicture = dto.ProfilePicture,
            Email = dto.Email,
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            DateCreated = dto.DateCreated
        };
    }
}
