using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : Controller
{
    private readonly IcommentService _commentService;

    public ILogger<CommentController> _logger;

    public CommentController(IcommentService commentservice, ILogger<CommentController> logger)
    {
        _commentService = commentservice;
        _logger = logger;
    }

    [HttpGet(Name = "GetAllComments")]
    public async Task<ActionResult<ICollection<CommentDTO>>> GetAllComments(int pageSize, int pageNumber)
    {
        var comments = await _commentService.GetAllCommentsAsync(pageSize, pageNumber);
        if (comments == null)
            return NotFound("No Comments were found");

        return Ok(comments);
    }


    [HttpGet("ReviewId={ReviewId}", Name = "GetAllCommentsByReviewId")]
    public async Task<ActionResult<ICollection<CommentDTO>>> GetAllCommentsByReviewId(int reviewId)
    {
        var comments = await _commentService.GetAllCommentsByReviewIdAsync(reviewId);
        if (comments == null)
            return NotFound("No Comments with that review id were found");

        return Ok(comments);
    }


    [HttpGet("Id={Id}", Name = "GetAllCommentsByUserId")]
    public async Task<ActionResult<ICollection<CommentDTO>>> GetAllCommentsByUserId(int userId)
    {
        var comments = await _commentService.GetAllCommentsByUserIdAsync(userId);
        if (comments == null)
            return NotFound("No Comments with that user id were found");

        return Ok(comments);
    }


    [HttpPost(Name = "AddNewComment")]
    public async Task<ActionResult<CommentDTO>> AddNewComment(CommentDTO dto)
    {
        var comment = await _commentService.AddNewCommentAsync(dto);
        if (comment == null)
            return BadRequest("The comment could not be added");

        return Ok(comment);
    }


    [HttpDelete(Name = "DeleteCommentById")]
    public async Task<ActionResult<CommentDTO>> DeleteCommentByIdAsync(int id)
    {
        var comment = await _commentService.DeleteCommentByIdAsync(id);
        if (comment == null)
            return BadRequest("Comments could not be deleted");

        return Ok(comment);
    }


    [HttpPut(Name = "UpdateCommentById")]
    public async Task<ActionResult<CommentDTO>> UpdateCommentById(int id, CommentDTO dto)
    {
        var comment = await _commentService.UpdateCommentAsync(id, dto);
        if (comment == null)
            return BadRequest("Comments could not be updated");

        return Ok(comment);
    }
}
