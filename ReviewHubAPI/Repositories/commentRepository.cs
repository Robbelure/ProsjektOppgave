using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories;

public class commentRepository : ICommentRepository
{
    public Task<ICollection<Comment>> GetAllCommentsAsync(int PageSize, int Pagenummer)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Comment>> GetAllCommentsByReviewIdAsync(int ReviewId)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Comment>> GetAllCommentsByUserIdAsync(int UserId)
    {
        throw new NotImplementedException();
    }
    public Task<Comment> GetCommentByIdAsync(int id)
    {
        throw new NotImplementedException();
    
    }
    public Task<Comment> AddNewCommentAsync(CommentDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<Comment> DeleteCommentByIdAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<Comment> UpdateCommentAsync(CommentDTO dto)
    {
        throw new NotImplementedException();
    }
}
