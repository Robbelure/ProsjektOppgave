using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers;

public class UserRegistrationResponseMapper : IMapper<UserEntity, UserRegistrationResponseDTO>
{
    public UserRegistrationResponseDTO MapToDTO(UserEntity entity)
    {
        return new UserRegistrationResponseDTO
        {
            UserID = entity.UserID,
            Username = entity.Username,
            Email = entity.Email,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            DateCreated = entity.DateCreated
        };
    }

    public UserEntity MapToEntity(UserRegistrationResponseDTO dto)
    {
        throw new NotImplementedException();
    }
}
