using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class ReviewRepository : IReviewRespository
    {
        public Task<Review> AddReview(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task<Review> DeleteReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Review>> GetAllReviews(int pagesize, int pagenummer)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Review> UpdateReviewById(Review entity)
        {
            throw new NotImplementedException();
        }
    }
}
