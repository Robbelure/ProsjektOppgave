using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface
{
    public interface IReviewRespository
    {
        public Task<ICollection<ReviewEntity>> GetAllReviews(int pagesize, int pagenummer);

        public Task<ReviewEntity> GetReviewById(int id);
        public Task<ReviewEntity> AddReview(ReviewEntity entity);
        public Task<ReviewEntity> UpdateReviewById(ReviewEntity entity);
        public Task<ReviewEntity> DeleteReviewById(int id);

    }
}
