using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Services;

public class ReviewPictureService : IReviewPictureService
{
    private readonly IReviewPictureRepository _reviewPictureRepository;
    private readonly IMapper<ReviewPicture, ReviewPictureDTO> _reviewpicmapper;
    private readonly ILogger<ReviewPictureService> _logger;

    public ReviewPictureService(IReviewPictureRepository reviewPictureRepository, 
        IMapper<ReviewPicture, ReviewPictureDTO> reviewpicmapper, 
        ILogger<ReviewPictureService> logger)
    {
        _reviewPictureRepository = reviewPictureRepository;
        _reviewpicmapper = reviewpicmapper;
        _logger = logger;
    }

    public async Task<string> AddReviewPicture(IFormFile file, int ReviewId)
    {
        var pic = await GetPictureBytesAsync(file);
        ReviewPicture reviewPicture = new ReviewPicture
        {
            ReviewId = ReviewId,
            Picture = pic,
            DateUpdated = DateTime.Now,
            DateCreated = DateTime.Now
        };
        _logger.LogInformation("Attempting to add review picture for ReviewId: {ReviewId}", ReviewId);
        var addedReviewPic = await _reviewPictureRepository.AddReviewPicture(reviewPicture);
        return addedReviewPic;
    }

    public async Task<ReviewPictureDTO> DeleteReviewPictureByReviewIdAsync(int ReviewId)
    {
        var reviewpic = await _reviewPictureRepository.DeleteReviewPictureByReviewIdAsync(ReviewId);

        return _reviewpicmapper.MapToDTO(reviewpic) ?? null!;
    }

    public async Task<ICollection<ReviewPictureDTO>> GetAllReviewPicturesAsync(int PageSize, int PageNummer)
    {
        var reviewpics = await _reviewPictureRepository.GetAllReviewPicturesAsync(PageSize, PageNummer);
        List<ReviewPictureDTO> reviewpicsDTO = new ();   
        
        if(reviewpics == null)
        {
            return null!;
        }

        foreach (var pic in reviewpics)
        {

            reviewpicsDTO.Add(_reviewpicmapper.MapToDTO(pic));
        }

        return reviewpicsDTO;

    }

    public async Task<ReviewPictureDTO?> GetReviewPictureByReviewIdAsync(int ReviewId)
    {
        var pic = await _reviewPictureRepository.GetReviewPictureByReviewIdAsync(ReviewId);
        if(pic == null)
        {  return null!; }

        return _reviewpicmapper.MapToDTO(pic);
    }

    private async Task<byte[]> GetPictureBytesAsync(IFormFile picture)
    {

        using (var memoryStream = new System.IO.MemoryStream())
        {
            await picture.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
