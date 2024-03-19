using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Mappers
{
    public class CommentMapper : IMapper<CommentEntity, CommentDTO>
    {
        public CommentDTO MapToDTO(CommentEntity entity)
        {
            return new CommentDTO
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ReviewId = entity.ReviewId,
                Title = entity.Title,
                Comment = entity.Comment,
                Created = entity.Created,
                Updated = entity.Updated,
            };
        }

        public CommentEntity MapToEntity(CommentDTO dto)
        {
            return new CommentEntity
            {
                Id = dto.Id,
                UserId = dto.UserId,
                ReviewId = dto.ReviewId,
                Title = dto.Title,
                Comment = dto.Comment,
                Created = dto.Created,
                Updated = dto.Updated,
            };
        }
    }
}
