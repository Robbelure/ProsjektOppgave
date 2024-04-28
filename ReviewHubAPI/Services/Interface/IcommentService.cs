using ReviewHubAPI.Models.DTO;

namespace ReviewHubAPI.Services.Interface;

public interface IcommentService
{
    public Task<ICollection<CommentDTO>> GetAllCommentsByReviewIdAsync(int ReviewId);
    public Task<ICollection<CommentDTO>> GetAllCommentsByUserIdAsync(int UserId);
    public Task<ICollection<CommentDTO>> GetAllCommentsAsync(int PageSize, int Pagenummer);
    public Task<CommentDTO> AddNewCommentAsync(CommentDTO dto);
    public Task<CommentDTO> UpdateCommentAsync(int Id, CommentDTO dto);
    public Task<CommentDTO> DeleteCommentByIdAsync(int Id);
}
