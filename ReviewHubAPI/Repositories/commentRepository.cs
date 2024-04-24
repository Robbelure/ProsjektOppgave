using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories;

public class commentRepository : ICommentRepository
{
    public Task<ICollection<Comment>> GetAllComents(int PageSize, int Pagenummer)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Comment>> GetAllComentsByReviewId(int ReviewId)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Comment>> GetAllComentsByUserId(int UserId)
    {
        throw new NotImplementedException();
    }
    public Task<Comment> GetCommentById(int id)
    {
        throw new NotImplementedException();
    
    }
    public Task<Comment> AddNewComment(CommentDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<Comment> DeleteCommentById(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<Comment> UpdateComment(CommentDTO dto)
    {
        throw new NotImplementedException();
    }
}
