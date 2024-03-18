
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

        public ReviewController(IReviewService reviewser)
        {
            _reviewser = reviewser;
        }
        [HttpGet(Name = "GetALlReviews")]
        public async Task<ActionResult<ICollection<ReviewDTO>>> GetAllReviews(int pagesize, int pagenummer)
        {
            var reviews =await  _reviewser.GetAllReviews(pagesize,pagenummer);

            if(reviews != null)
            {
                return Ok(reviews);

            }
            return NotFound("There were no reviews in the database");

        }

        [HttpGet("{id}", Name ="GetReviewById")]
        public async Task<ActionResult<ReviewDTO?>> GetReviewById(int id)
        { 
            var review = await _reviewser.GetReviewById(id);
            if (review != null)
            {
                return Ok(review);
            }

            return NotFound("Not reviews with that id was found");

        }

        [HttpPost(Name = "AddNewReview")]

        public async Task<ActionResult<ReviewDTO>> AddReview(ReviewDTO dto)
        {  
            var review = await _reviewser.AddReview(dto);
            if (review != null)
            {
                return Ok(review);
            }
            return BadRequest("Movie could not be added");


        }

        [HttpPut(Name = "UpdateReview")]
        public async Task<ActionResult<ReviewDTO>> UpdateReviewById(int id, ReviewDTO dto)
        {
            var review = await _reviewser.UpdateReviewById(id, dto);
            if (review != null)
            {
                return Ok(review);
            }

            return BadRequest("The movie could not be updated");

        }

        [HttpDelete ("{id}", Name = "DeleteReviewById")]
        public async Task<ActionResult<ReviewDTO>> DeleteReviewById(int id)
        { 
            var review = await _reviewser.DeleteReviewById(id);
            if (review != null)
            {
                return Ok(review);
            }

            return BadRequest("The movie could not be deleted");
        }

    }
        
}
