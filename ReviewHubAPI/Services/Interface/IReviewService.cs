using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Services.Interface
{
    public interface IReviewService
    {
        public Task<ICollection<ReviewDTO>> GetAllReviews(int pagesize, int pagenummer);
        public Task<ReviewDTO?> GetReviewById(int id);
        public Task<ReviewDTO> AddReview(ReviewDTO dto);
        public Task<ReviewDTO> UpdateReviewById(int id, ReviewDTO dto);
        public Task<ReviewDTO> DeleteReviewById(int id);
    }
}
