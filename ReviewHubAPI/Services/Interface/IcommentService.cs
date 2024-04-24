using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Services.Interface;

public interface IcommentService
{
    public Task<ICollection<CommentDTO>> GetAllComentsByReviewId(int ReviewId);
    public Task<ICollection<CommentDTO>> GetAllComentsByUserId(int UserId);
    public Task<ICollection<CommentDTO>> GetAllComents(int PageSize, int Pagenummer);
    public Task<CommentDTO> AddNewComment(CommentDTO dto);
    public Task<CommentDTO> UpdateComment(int Id, CommentDTO dto);
    public Task<CommentDTO> DeleteCommentById(int Id);
}
