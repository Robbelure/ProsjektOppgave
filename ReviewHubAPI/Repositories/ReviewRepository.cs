using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class ReviewRepository : IReviewRespository
    {
        public Task<ReviewEntity> AddReview(ReviewEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewEntity> DeleteReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ReviewEntity>> GetAllReviews(int pagesize, int pagenummer)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewEntity> GetReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewEntity> UpdateReviewById(ReviewEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
