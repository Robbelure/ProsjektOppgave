using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;

namespace ReviewHubAPI.Repositories
{
    public class ReviewPictureRepository : IReviewPictureRepository
    {
        public Task<string> AddReviewPicture(ReviewPictureEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewPictureEntity> DeleteReviewPictureByReviewIdAsync(int ReviewId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ReviewPictureEntity>> GetAllReviewPicturesAsync(int PageSize, int PageNummer)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewPictureEntity> GetReviewPictureByReviewIdAsync(int ReviewId)
        {
            throw new NotImplementedException();
        }
    }
}
