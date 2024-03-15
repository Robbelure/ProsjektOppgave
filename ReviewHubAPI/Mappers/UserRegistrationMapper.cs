using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Utilities;

namespace ReviewHubAPI.Mappers;

public class UserRegistrationMapper : IMapper<UserEntity, UserRegistrationDTO>
{
    public UserRegistrationDTO MapToDTO(UserEntity entity)
    {
        throw new NotImplementedException();
    }

    public UserEntity MapToEntity(UserRegistrationDTO dto)
    {
        return new UserEntity
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = PasswordHelper.HashPassword(dto.Password),
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            DateCreated = DateTime.UtcNow
        };
    }


}
