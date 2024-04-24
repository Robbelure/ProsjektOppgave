using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers;

public class CommentMapper : IMapper<Comment, CommentDTO>
{
    public CommentDTO MapToDTO(Comment entity)
    {
        return new CommentDTO
        {
            Id = entity.Id,
            UserId = entity.UserId,
            ReviewId = entity.ReviewId,
            Title = entity.Title,
            Comment = entity.CommentText,
            Created = entity.Created,
            Updated = entity.Updated,
        };
    }

    public Comment MapToEntity(CommentDTO dto)
    {
        return new Comment
        {
            Id = dto.Id,
            UserId = dto.UserId,
            ReviewId = dto.ReviewId,
            Title = dto.Title,
            CommentText = dto.Comment,
            Created = dto.Created,
            Updated = dto.Updated,
        };
    }
}
