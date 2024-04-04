using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Utilities;

namespace ReviewHubAPI.Mappers;

public class UserRegistrationMapper : IMapper<User, UserRegistrationDTO>
{
    public UserRegistrationDTO MapToDTO(User entity)
    {
        throw new NotImplementedException();
    }

    public User MapToEntity(UserRegistrationDTO dto)
    {
        return new User
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
