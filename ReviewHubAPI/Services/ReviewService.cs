using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRespository _reviewrep;
        private readonly IMapper<ReviewEntity, ReviewDTO> _reviewmapper;

        public ReviewService(IReviewRespository ReviewRep , IMapper<ReviewEntity, ReviewDTO> Reviewmapper)
        {
            _reviewrep = ReviewRep;
            _reviewmapper = Reviewmapper;
        }

        public async Task<ICollection<ReviewDTO>> GetAllReviews(int pagesize, int pagenummer)
        {
            var reviewsentity = await _reviewrep.GetAllReviews(pagenummer,pagesize);
            var reviews = new List<ReviewDTO>();

            if (reviewsentity != null)
            {
                foreach (var review in reviewsentity)
                {
                    reviews.Add(_reviewmapper.MapToDTO(review));
                }
            }

            return reviews;
        }

        public async Task<ReviewDTO?> GetReviewById(int id)
        {
            var review = await _reviewrep.GetReviewById(id);

            return _reviewmapper.MapToDTO(review) ?? null;
        }

        public async Task<ReviewDTO> AddReview(ReviewDTO dto)
        {
            var reviewtoadd = await _reviewrep.AddReview(_reviewmapper.MapToEntity(dto));

            return _reviewmapper.MapToDTO(reviewtoadd)?? null!;

        }

        public async Task<ReviewDTO> DeleteReviewById(int id)
        {
            var reviewtodelete = await _reviewrep.DeleteReviewById(id);

            return _reviewmapper.MapToDTO(reviewtodelete) ?? null!;
        }

        public async Task<ReviewDTO> UpdateReviewById(int id, ReviewDTO dto)
        {
            var reviewtoupdate = await _reviewrep.GetReviewById(id);
             if (reviewtoupdate != null)
            {
                reviewtoupdate.Id = id;
                reviewtoupdate.MovieId = dto.MovieId;
                reviewtoupdate.Userid = dto.Userid;
                reviewtoupdate.Text = dto.Text;
                reviewtoupdate.Title = dto.Title;
                reviewtoupdate.MoviePicture = dto.MoviePicture;

                await _reviewrep.UpdateReviewById(reviewtoupdate);
                return _reviewmapper.MapToDTO(reviewtoupdate);

            }

            return null!;
        }
    }
}
