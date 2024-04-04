using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers;

public class UserMapper : IMapper<User, UserDTO>
{
    public UserDTO MapToDTO(User entity)
    {
        return new UserDTO
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

    public User MapToEntity(UserDTO dto)
    {
        return new User
        {
            Id = dto.Id,
            Username = dto.Username,
            Email = dto.Email,
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            DateCreated = dto.DateCreated,
            IsAdmin = dto.IsAdmin
        };
    }
}
