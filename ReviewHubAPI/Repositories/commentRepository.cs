using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class commentRepository : IcommentRepository
    {
        public Task<ICollection<CommentEntity>> GetAllComents(int PageSize, int Pagenummer)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<CommentEntity>> GetAllComentsByReviewId(int ReviewId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<CommentEntity>> GetAllComentsByUserId(int UserId)
        {
            throw new NotImplementedException();
        }
        public Task<CommentEntity> GetCommentById(int id)
        {
            throw new NotImplementedException();
        
        }
        public Task<CommentEntity> AddNewComment(CommentDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<CommentEntity> DeleteCommentById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<CommentEntity> UpdateComment(CommentDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
