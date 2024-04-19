
using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewser;

        public ILogger<ReviewController> _logger;

        public ReviewController(IReviewService reviewser, ILogger<ReviewController> logger)
        {
            _reviewser = reviewser;
            _logger = logger;
        }
        [HttpGet(Name = "GetALlReviews")]
        public async Task<ActionResult<ICollection<ReviewDTO>>> GetAllReviews(int pagesize, int pagenummer)
        {
            try
            {
                var reviews = await _reviewser.GetAllReviews(pagesize, pagenummer);

                if (reviews != null)
                {
                    return Ok(reviews);

                }
                return NotFound("There were no reviews in the database");
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on get all reviews in the ReviewController: {ex}", ex);

                return StatusCode(500, $"An error occurred while trying to get all reviews");
            }
        }



        [HttpGet("Id={id}", Name = "GetReviewById")]
        public async Task<ActionResult<ReviewDTO?>> GetReviewById(int id)
        {
            try
            {
                var review = await _reviewser.GetReviewById(id);
                if (review != null)
                {
                    return Ok(review);
                }

                return NotFound("Not reviews with that id was found");
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on get review by id in the ReviewController:{ex}", ex);

                return StatusCode(500, $"An error occurred while trying to get review by id");

            }

        }

        [HttpPost(Name = "AddNewReview")]

        //TODO Her trenger vi ikke å få tilbake ReviewDTO kun string
        public async Task<ActionResult<ReviewDTO>> AddReview(ReviewDTO dto)
        {
            try
            {
                var review = await _reviewser.AddReview(dto);
                if (review != null)
                {
                    return Ok(review);
                }
                return BadRequest("Movie could not be added");
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on add review in the ReviewController: {ex}", ex);

                return StatusCode(500, $"An error occurred while trying to add a review");

            }


        }

        [HttpPut("Id={id}", Name = "UpdateReview")]
        public async Task<ActionResult<ReviewDTO>> UpdateReviewById(int id, ReviewDTO dto)
        {
            try
            {
                var review = await _reviewser.UpdateReviewById(id, dto);
                if (review != null)
                {
                    return Ok(review);
                }

                return BadRequest("The movie could not be updated");
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on update review in the ReviewController: {ex}", ex);

                return StatusCode(500, $"An error occurred while trying to update review");

            }

        }

        [HttpDelete("Id={id}", Name = "DeleteReviewById")]
        public async Task<ActionResult<ReviewDTO>> DeleteReviewById(int id)
        {
            try
            {
                var review = await _reviewser.DeleteReviewById(id);
                if (review != null)
                {
                    return Ok(review);
                }

                return BadRequest("The movie could not be deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on update review in the ReviewController: {ex}", ex);

                return StatusCode(500, $"An error occurred while trying to update review");

            }
        }

        [HttpGet("MovieId={movieId}", Name = "GetReviewByMovieId")]
        public async Task<ActionResult<ReviewDTO?>> GetReviewByMovieID(int movieId)
        {
            try
            {
                var review = await _reviewser.GetReviewByMovieId(movieId);
                if (review != null)
                {
                    return Ok(review);
                }

                return BadRequest("There were no reviews with that movieid");
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on get reviews by movie id in the ReviewController: {ex}", ex);

                return StatusCode(500, $"An error occurred while trying to get reviews by movie id");

            }


        }

        [HttpGet("UserId={userId}", Name = "GetReviewByUserId")]
        public async Task<ActionResult<ReviewDTO?>> GetReviewByUserID(int userId)
        {
            try
            {
                var review = await _reviewser.GetReviewByUserId(userId);
                if (review != null)
                {
                    return Ok(review);
                }

                return BadRequest("There were no reviews with that user id");
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on get reviews by user id in the ReviewController: {ex}", ex);

                return StatusCode(500, $"An error occurred while trying to get reviews by user id");

            }
        }

    }
}
