using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Services;

public class CommentService : IcommentService
{
    private ICommentRepository _commentRep;
    private IMapper<Comment, CommentDTO> _commentmapper;
    public CommentService(ICommentRepository commentRep, IMapper<Comment,CommentDTO> commentmapper)
    {
        _commentRep = commentRep;
        _commentmapper = commentmapper;
    }

    public async Task<CommentDTO> AddNewCommentAsync(CommentDTO dto)
    {
        dto.Created = DateTime.Now;
        dto.Updated = DateTime.Now;
        var comment = await _commentRep.AddNewCommentAsync(_commentmapper.MapToEntity(dto));

        return _commentmapper.MapToDTO(comment) ?? null!;
    }

    public async Task<ICollection<CommentDTO>> GetAllCommentsAsync(int PageSize, int Pagenummer)
    {
        var comments = await _commentRep.GetAllCommentsAsync(PageSize, Pagenummer);
        ICollection<CommentDTO> commentsdto = new List<CommentDTO>();

        if(comments != null)
        {
            foreach (var comment in comments) 
            {
                commentsdto.Add(_commentmapper.MapToDTO(comment));
            }
        }

        return commentsdto ?? null!;
    }

    public async Task<ICollection<CommentDTO>> GetAllCommentsByReviewIdAsync(int ReviewId)
    {
        var comments = await _commentRep.GetAllCommentsByReviewIdAsync(ReviewId);
        ICollection<CommentDTO> commentsdto = new List<CommentDTO>();

        if (comments.Count > 0)
        {
            foreach (var comment in comments)
            {
                commentsdto.Add(_commentmapper.MapToDTO(comment));
            }
        }

        return commentsdto ?? null!;
    }

    public async Task<ICollection<CommentDTO>> GetAllCommentsByUserIdAsync(int UserId)
    {
        var comments = await _commentRep.GetAllCommentsByUserIdAsync(UserId);
        ICollection<CommentDTO> commentsdto = new List<CommentDTO>();

        if (comments != null)
        {
            foreach (var comment in comments)
            {
                commentsdto.Add(_commentmapper.MapToDTO(comment));
            }
        }

        return commentsdto ?? null!;
    }

    public async Task<CommentDTO> DeleteCommentByIdAsync(int Id)
    {
        var comment = await _commentRep.GetCommentByIdAsync(Id);

        if (comment != null)
        {
            await _commentRep.DeleteCommentByIdAsync(Id);

        }

        return _commentmapper.MapToDTO(comment!) ?? null!;
    }

    public async Task<CommentDTO> UpdateCommentAsync(int Id, CommentDTO dto)
    {
        var updatedate = DateTime.Now;
        var comment = await _commentRep.GetCommentByIdAsync(Id);

        if (comment != null)
        {
            comment.Updated = updatedate;
            comment.Title = dto.Title;
            comment.CommentText = dto.Comment;
        }

        return _commentmapper.MapToDTO(comment!) ?? null!;
    }
}
