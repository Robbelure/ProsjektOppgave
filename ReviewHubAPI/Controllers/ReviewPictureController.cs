using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewPictureController : Controller
{
    private readonly IReviewPictureService _reviewPictureService;
    private readonly ILogger<IReviewPictureService> _logger;

    public ReviewPictureController(IReviewPictureService reviewPictureService, ILogger<IReviewPictureService> logger)
    {
        _reviewPictureService = reviewPictureService;
        _logger = logger;
    }

    [HttpPost("Id={ReviewId}", Name = "AddNewReviewPicture")]
    public async Task<ActionResult<string>> AddReviewPictures(IFormFile file, int ReviewId)
    {
        var message = await _reviewPictureService.AddReviewPicture(file, ReviewId);
        if (!string.IsNullOrEmpty(message))
        {
            _logger.LogInformation("Review picture added: {ReviewId}", ReviewId);
            return Ok(message);
        }
        _logger.LogWarning("Error occurred while trying to add review picture for ReviewId: {ReviewId}", ReviewId);
        return BadRequest("An error occurred while trying to add review picture");
    }


    [HttpGet (Name = "GetAllReviewPictures")]
    public async Task<ActionResult<ICollection<ReviewPictureDTO>>> GetAllReviewPicturesAsync(int PageSize, int PageNummer)
    {
        try
        {
            var pics = await _reviewPictureService.GetAllReviewPicturesAsync(PageSize, PageNummer);

            if (pics == null)
            {
                return NotFound("No Review Pictures were found");
            }

            return Ok(pics);

        }
        catch (Exception ex)
        {
            _logger.LogError("An Error occurred on get all review pictures in the ReviewPictureController : {ex}", ex);

            return StatusCode(500, $"An error occurred while trying to get all review pictures");

        }
    }

    [HttpDelete ("ReviewId={ReviewId}", Name = "DeleteReviewPictureByReviewId")]

    public async Task<ActionResult<ReviewPictureDTO>> DeleteReviewPictureByReviewIdAsync(int ReviewId)
    {
        try
        {
            var pic = await _reviewPictureService.DeleteReviewPictureByReviewIdAsync(ReviewId);

            if (pic == null)
            {
                return BadRequest("Review picture could not be deleted");
            }
            return Ok(pic);
        }
        catch (Exception ex)
        {
            _logger.LogError("An Error occurred on delete review pictures by ReviewId in the ReviewPictureController : {ex}", ex);

            return StatusCode(500, $"An error occurred while trying to delete review picture by ReviewId picture");

        }
    }

    [HttpGet("Id={ReviewId}", Name = "GetReviewPictureByReviewId")]
        public async Task<ActionResult<ReviewPictureDTO?>> GetReviewPictureByReviewIddAsync(int ReviewId)
        {
            try { 
            var pic = await _reviewPictureService.GetReviewPictureByReviewIdAsync(ReviewId);

            if(pic == null)
            {
                return NotFound("the review picture was not found");
            }
            return Ok(pic);
            }
            catch(Exception ex)
            {
                _logger.LogError("An Error occurred on get review picture by ReviewId in the ReviewPictureController : {ex}", ex);

                return StatusCode(500, $"An error occurred while trying get review picture by ReviewId");


            }

        }

}
