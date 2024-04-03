using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface
{
    public interface IReviewPictureRepository
    {
        public Task<string> AddReviewPicture(ReviewPictureEntity entity);
        public Task<ICollection<ReviewPictureEntity>> GetAllReviewPicturesAsync(int PageSize, int PageNummer);

        public Task<ReviewPictureEntity> GetReviewPictureByReviewIdAsync(int ReviewId);

        public Task<ReviewPictureEntity> DeleteReviewPictureByReviewIdAsync(int ReviewId);
    }
}
