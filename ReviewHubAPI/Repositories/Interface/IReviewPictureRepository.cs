using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Repositories.Interface
{
    public interface IReviewPictureRepository
    {
        public Task<string> AddReviewPicture(ReviewPicture entity);
        public Task<ICollection<ReviewPicture>> GetAllReviewPicturesAsync(int PageSize, int PageNummer);

        public Task<ReviewPicture> GetReviewPictureByReviewIdAsync(int ReviewId);

        public Task<ReviewPicture> DeleteReviewPictureByReviewIdAsync(int ReviewId);
    }
}
