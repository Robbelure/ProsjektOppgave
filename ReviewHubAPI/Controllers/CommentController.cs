using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly IcommentService _commentservice;

        public ILogger<CommentController> _logger;

        public CommentController(IcommentService commentservice, ILogger<CommentController> logger)
        {
            _commentservice = commentservice;
            _logger = logger;
        }

        [HttpGet(Name ="GetAllComments")]

        public async Task<ActionResult<ICollection<CommentDTO>>> GetAllComments(int PageSize, int PageNummer)
        {
            try 
            { 
                var comments = await _commentservice.GetAllComents(PageSize, PageNummer);
                if (comments == null)
                { 
                    return NotFound("No Comments were found"); 
                }

                return Ok(comments);
            }
            catch (Exception ex) 
            {
                _logger.LogError("An Error occurred on get all comments in the CommentController: {ex}", ex);

                return StatusCode(500, $"An error occurred while trying to get all comments");
            }
        }

        [HttpGet ("ReviewId={ReviewId}", Name = "GetAllComentsByReviewId")]
        public async Task<ActionResult<ICollection<CommentDTO>>> GetAllComentsByReviewId(int ReviewId)
        {
            try 
            { 
                var comments = await _commentservice.GetAllComentsByReviewId(ReviewId);
                if (comments == null)
                {
                 return NotFound("No Comments with that review id was found");
                }

                return Ok(comments);
            }
            catch(Exception ex)
            {
                _logger.LogError("An Error occurred on get all comments by review id in the CommentController: {ex}", ex);

                return StatusCode(500, $"An error occurred while trying to get all comments by the review id ");

            }

        }

        [HttpGet("Id={Id}", Name = "GetAllComentsByUserId")]
        public async Task<ActionResult<ICollection<CommentDTO>>> GetAllComentsByUserId(int UserId)
        {
            try
            { 
                var comments = await _commentservice.GetAllComentsByUserId(UserId);
                if (comments == null)
                {
                    return NotFound("No Comments with that user id was found");
                }

                return Ok(comments);
            }
            catch(Exception ex)
            {
                _logger.LogError("An Error occurred on get all comments by review id in the CommentController:{ex}", ex);
                return StatusCode(500, $"An error occurred while trying to get all comments by the review id ");

            }
        }

        [HttpPost(Name ="AddNewComment")]
        public async Task<ActionResult<CommentDTO>> AddNewComment(CommentDTO dto)
        {
            try
            { 
                var comments = await _commentservice.AddNewComment(dto);
                if (comments == null)
                {
                    return BadRequest("The comment could not be added");
                }

                return Ok(comments);
            }
            catch(Exception ex)
            {
                _logger.LogError("An Error occurred on get add new comment in the CommentController: {ex}", ex);
                return StatusCode(500, $"An error occurred while trying to add a new comment");
            }
        }

        [HttpDelete(Name = "DeleteCommentById")]
        public async Task<ActionResult<CommentDTO>> DeleteCommentById(int id)
        {
            try
            { 
                var comment = await _commentservice.DeleteCommentById(id);
                if (comment == null)
                {
                    return BadRequest("Comments Could not be deleted");
                }
                return Ok(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on delete comment in the CommentController: {ex}", ex);
                return StatusCode(500, $"An error occurred while trying to delete comment");
            }
        }

        [HttpPut (Name ="UpdateCommentById")]
        
        public async Task<ActionResult<CommentDTO>> UpdateCommentById(int Id, CommentDTO dto)
        {
            try 
            { 
                var comment = await _commentservice.UpdateComment(Id, dto);
                if (comment == null)
                {
                    return BadRequest("Comments Could not be updated");
                }
                return Ok(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError("An Error occurred on update a comment in the CommentController:{ex}", ex);
                return StatusCode(500, $"An error occurred while trying to update the comment");
            }
        }

    }

}
