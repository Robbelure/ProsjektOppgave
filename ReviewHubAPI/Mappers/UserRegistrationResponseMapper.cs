using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers;

public class UserRegistrationResponseMapper : IMapper<User, UserRegistrationResponseDTO>
{
    public UserRegistrationResponseDTO MapToDTO(User entity)
    {
        return new UserRegistrationResponseDTO
        {
            Id = entity.Id,
            Username = entity.Username,
            Email = entity.Email,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            DateCreated = entity.DateCreated
        };
    }

    public User MapToEntity(UserRegistrationResponseDTO dto)
    {
        throw new NotImplementedException();
    }
}
