using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Data;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories;

public class commentRepository : ICommentRepository
{
    private readonly ReviewHubDbContext _dbcontext;

    public commentRepository(ReviewHubDbContext dbContext)
    {
        _dbcontext = dbContext;
    }
    public Task<ICollection<Comment>> GetAllCommentsAsync(int PageSize, int Pagenummer)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Comment>> GetAllCommentsByReviewIdAsync(int ReviewId)
    {
        var comments = await _dbcontext.Comments.Where(x => x.ReviewId == ReviewId).ToListAsync();
        
        if (comments.Count == 0) 
        {
            return null!;
        }

        return comments;
    }

    public Task<ICollection<Comment>> GetAllCommentsByUserIdAsync(int UserId)
    {
        throw new NotImplementedException();
    }
    public Task<Comment> GetCommentByIdAsync(int id)
    {
        throw new NotImplementedException();
    
    }
    public async Task<Comment> AddNewCommentAsync(Comment model)
    {
        var comment = await _dbcontext.Comments.AddAsync(model);
        await _dbcontext.SaveChangesAsync();

        if(comment == null)
        {
            return null!;
        }

        return comment.Entity;



        
    }

    public Task<Comment> DeleteCommentByIdAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<Comment> UpdateCommentAsync(Comment model)
    {
        throw new NotImplementedException();
    }
}
