using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface
{
    public interface ICommentRepository
    {
        public Task<ICollection<Comment>> GetAllComentsByReviewId(int ReviewId);
        public Task<ICollection<Comment>> GetAllComentsByUserId(int UserId);
        public Task<ICollection<Comment>> GetAllComents(int PageSize, int Pagenummer);
        public Task<Comment> GetCommentById(int id);
        public Task<Comment> AddNewComment(CommentDTO dto);
        public Task<Comment> UpdateComment(CommentDTO dto);
        public Task<Comment> DeleteCommentById(int id);


    }
}
