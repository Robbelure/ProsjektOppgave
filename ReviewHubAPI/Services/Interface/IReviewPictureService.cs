using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;

namespace ReviewHubAPI.Services.Interface
{
    public interface IReviewPictureService
    {
        public Task<ICollection<ReviewPictureDTO>> GetAllReviewPicturesAsync(int PageSize, int PageNummer);

        public Task<ReviewPictureDTO> GetReviewPictureByReviewIdAsync(int ReviewId);

        public Task<ReviewPictureDTO> DeleteReviewPictureByReviewIdAsync(int ReviewId);
    }
}
