using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class ReviewPictureRepository : IReviewPictureRepository
    {
        public Task<string> AddReviewPicture(ReviewPicture entity)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewPicture> DeleteReviewPictureByReviewIdAsync(int ReviewId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ReviewPicture>> GetAllReviewPicturesAsync(int PageSize, int PageNummer)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewPicture> GetReviewPictureByReviewIdAsync(int ReviewId)
        {
            throw new NotImplementedException();
        }
    }
}
