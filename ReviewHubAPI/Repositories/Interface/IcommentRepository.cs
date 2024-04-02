using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface
{
    public interface ICommentRepository
    {
        public Task<ICollection<CommentEntity>> GetAllComentsByReviewId(int ReviewId);
        public Task<ICollection<CommentEntity>> GetAllComentsByUserId(int UserId);
        public Task<ICollection<CommentEntity>> GetAllComents(int PageSize, int Pagenummer);
        public Task<CommentEntity> GetCommentById(int id);
        public Task<CommentEntity> AddNewComment(CommentDTO dto);
        public Task<CommentEntity> UpdateComment(CommentDTO dto);
        public Task<CommentEntity> DeleteCommentById(int id);


    }
}
