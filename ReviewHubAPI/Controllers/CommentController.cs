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
        private IcommentService _commentservice;

        public CommentController(IcommentService commentservice)
        {
            _commentservice = commentservice;
        }

        [HttpGet(Name ="GetAllComments")]

        public async Task<ActionResult<ICollection<CommentDTO>>> GetAllComments(int PageSize, int PageNummer)
        {
           var comments = await _commentservice.GetAllComents(PageSize, PageNummer);
            if (comments == null)
            { 
                return NotFound("No Comments were found"); 
            }

            return Ok(comments);
        }

        [HttpGet ("ReviewId={ReviewId}", Name = "GetAllComentsByReviewId")]
        public async Task<ActionResult<ICollection<CommentDTO>>> GetAllComentsByReviewId(int ReviewId)
        {
            var comments = await _commentservice.GetAllComentsByReviewId(ReviewId);
            if (comments == null)
            {
                return NotFound("No Comment with that review id was found");
            }

            return Ok(comments);

        }

        [HttpGet("Userid={UserId}", Name = "GetAllComentsByUserId")]
        public async Task<ActionResult<ICollection<CommentDTO>>> GetAllComentsByUserId(int UserId)
        {
            var comments = await _commentservice.GetAllComentsByUserId(UserId);
            if (comments == null)
            {
                return NotFound("No Comment with that user id was found");
            }

            return Ok(comments);
        }

        [HttpPost(Name ="AddNewComment")]
        public async Task<ActionResult<CommentDTO>> AddNewComment(CommentDTO dto)
        {
            var comments = await _commentservice.AddNewComment(dto);
            if (comments == null)
            {
                return BadRequest("The comment could not be added");
            }

            return Ok(comments);


        }

        [HttpDelete(Name = "DeleteCommentById")]
        public async Task<ActionResult<CommentDTO>> DeleteCommentById(int id)
        {
            var comment = await _commentservice.DeleteCommentById(id);


            if (comment == null)
            {
                return BadRequest("Comment Could not be deleted");
            }

            return Ok(comment);
        }

        [HttpPut (Name ="UpdateCommentById")]
        
        public async Task<ActionResult<CommentDTO>> UpdateCommentById(int Id, CommentDTO dto)
        {
            var comment = await _commentservice.UpdateComment(Id, dto);
           
            if (comment == null)
            {
                return BadRequest("Comment Could not be updated");
            }

            return Ok(comment);
        }

    }



    
}
