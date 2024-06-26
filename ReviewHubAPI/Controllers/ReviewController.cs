﻿using Microsoft.AspNetCore.Mvc;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : Controller
{
    private readonly IReviewService _reviewService;

    public ILogger<ReviewController> _logger;

    public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
    {
        _reviewService = reviewService;
        _logger = logger;
    }


    [HttpGet(Name = "GetALlReviews")]
    public async Task<ActionResult<ICollection<ReviewDTO>>> GetAllReviews(int pagesize, int pagenummer)
    {
        var reviews = await _reviewService.GetAllReviews(pagesize, pagenummer);
        if (reviews.Count > 0)
            return Ok(reviews);  
        
        return NotFound("There were no reviews in the database");
    }


    [HttpGet("Id={id}", Name = "GetReviewById")]
    public async Task<ActionResult<ReviewDTO?>> GetReviewById(int id)
    {
        var review = await _reviewService.GetReviewById(id);
        if (review != null)
            return Ok(review);

        return NotFound("Not reviews with that id was found");
    }


    [HttpPost(Name = "AddNewReview")]
    public async Task<ActionResult<string>> AddReview(ReviewDTO dto)
    {
        var review = await _reviewService.AddReview(dto);
        if (review != null)
        {
            _logger.LogInformation("Review added successfully.");
            return Ok(review);
        }
        _logger.LogWarning("Movie could not be added.");
        return BadRequest("Movie could not be added.");
    }


    [HttpPut("Id={id}", Name = "UpdateReview")]
    public async Task<ActionResult<ReviewDTO>> UpdateReviewById(int id, ReviewDTO dto)
    {
        var review = await _reviewService.UpdateReviewById(id, dto);
        if (review != null)
            return Ok(review);

        return BadRequest("The movie could not be updated");        
    }


    [HttpDelete("Id={id}", Name = "DeleteReviewById")]
    public async Task<ActionResult<ReviewDTO>> DeleteReviewById(int id)
    {
        var review = await _reviewService.DeleteReviewById(id);
        if (review != null)
            return Ok(review);

        return BadRequest("The movie could not be deleted");        
    }


    [HttpGet("MovieId={movieId}", Name = "GetReviewByMovieId")]
    public async Task<ActionResult<ReviewDTO?>> GetReviewByMovieID(int movieId)
    {
        var review = await _reviewService.GetReviewByMovieId(movieId);
        if (review != null)
            return Ok(review);

        return BadRequest("There were no reviews with that movieid");       
    }


    [HttpGet("UserId={userId}", Name = "GetReviewByUserId")]
    public async Task<ActionResult<ReviewDTO?>> GetReviewByUserID(int userId)
    {
        var review = await _reviewService.GetReviewByUserId(userId);
        if (review != null)
            return Ok(review);

        return BadRequest("There were no reviews with that user id");
    }
}
