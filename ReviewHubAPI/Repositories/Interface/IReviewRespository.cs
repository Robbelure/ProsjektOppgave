using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface;

public interface IReviewRespository
{
    public Task<ICollection<Review>> GetAllReviews(int pagesize, int pagenummer);
    public Task<Review> GetReviewById(int id);
    public Task<Review> AddReview(Review entity);
    public Task<Review> UpdateReviewById(Review entity);
    public Task<Review> DeleteReviewById(int id);
    public Task<ICollection<Review>> GetReviewByMovieId(int ByMovieId);
    public Task<ICollection<Review>> GetReviewByUserId(int UserId);
}
