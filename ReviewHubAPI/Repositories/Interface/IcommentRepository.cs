using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface;

public interface ICommentRepository
{
    public Task<ICollection<Comment>> GetAllCommentsByReviewIdAsync(int ReviewId);
    public Task<ICollection<Comment>> GetAllCommentsByUserIdAsync(int UserId);
    public Task<ICollection<Comment>> GetAllCommentsAsync(int PageSize, int Pagenummer);
    public Task<Comment> GetCommentByIdAsync(int id);
    public Task<Comment> AddNewCommentAsync(CommentDTO dto);
    public Task<Comment> UpdateCommentAsync(CommentDTO dto);
    public Task<Comment> DeleteCommentByIdAsync(int id);
}
