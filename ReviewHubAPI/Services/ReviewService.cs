using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRespository _reviewrep;
    private readonly IMapper<Review, ReviewDTO> _reviewmapper;

    public ReviewService(IReviewRespository ReviewRep , IMapper<Review, ReviewDTO> Reviewmapper)
    {
        _reviewrep = ReviewRep;
        _reviewmapper = Reviewmapper;
    }

    public async Task<ICollection<ReviewDTO>> GetAllReviews(int pagesize, int pagenummer)
    {
        var reviewsentity = await _reviewrep.GetAllReviews(pagenummer,pagesize);
        var reviews = new List<ReviewDTO>();

        if (reviewsentity.Count>0)
        {
            foreach (var review in reviewsentity)
            {
                reviews.Add(_reviewmapper.MapToDTO(review));
            }
        }

        return reviews ?? null!;
    }

    public async Task<ReviewDTO?> GetReviewById(int id)
    {
        var review = await _reviewrep.GetReviewById(id);

        if (review != null)
        {
            return _reviewmapper.MapToDTO(review);

        }

        return null;
    }

    public async Task<ReviewDTO> AddReview(ReviewDTO dto)
    {
        dto.DateCreated = DateTime.Now;
        dto.DateUpdated = DateTime.Now;
        var reviewtoadd = await _reviewrep.AddReview(_reviewmapper.MapToEntity(dto));

        return _reviewmapper.MapToDTO(reviewtoadd)?? null!;

    }

    public async Task<ReviewDTO> DeleteReviewById(int id)
    {
        var reviewtodelete = await _reviewrep.DeleteReviewById(id);

        if(reviewtodelete != null)
        {
            return _reviewmapper.MapToDTO(reviewtodelete);
        }
         return null!;
       
    }

    public async Task<ReviewDTO> UpdateReviewById(int id, ReviewDTO dto)
    {
        var reviewtoupdate = await _reviewrep.GetReviewById(id);
         if (reviewtoupdate != null)
         {
            reviewtoupdate.Id = id;
            reviewtoupdate.MovieId = dto.MovieId;
            reviewtoupdate.UserId = dto.UserId;
            reviewtoupdate.Text = dto.Text;
            reviewtoupdate.Title = dto.Title;
            reviewtoupdate.DateUpdated = DateTime.Now;
         
            await _reviewrep.UpdateReviewById(reviewtoupdate);
            return _reviewmapper.MapToDTO(reviewtoupdate);
         }

        return null!;
    }

    public async Task<ICollection<ReviewDTO>> GetReviewByMovieId(int ByMovieId)
    {
        var reviewsentity = await _reviewrep.GetReviewByMovieId(ByMovieId);
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

    public async Task<ICollection<ReviewDTO>> GetReviewByUserId(int UserId)
    {
        var reviewsentity = await _reviewrep.GetReviewByUserId(UserId);
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
}
