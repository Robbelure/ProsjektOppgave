using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Services;

public class ReviewPictureService : IReviewPictureService
{
    private readonly IReviewPictureRepository _reviewpicturerepository;
    private readonly IMapper<ReviewPicture, ReviewPictureDTO> _reviewpicmapper;

    public ReviewPictureService(IReviewPictureRepository reviewpicturerepository, IMapper<ReviewPicture, ReviewPictureDTO> reviewpicmapper)
    {
        _reviewpicturerepository = reviewpicturerepository;
        _reviewpicmapper = reviewpicmapper;
    }

    public async Task<string> AddReviewPicture(IFormFile file, int ReviewId)
    {
        var pic = await GetPictureBytesAsync(file);
        ReviewPicture rewpicture = new ReviewPicture 
        { 
            ReviewId = ReviewId,
            Picture = pic,
        
        };
        var addedRewPic = await  _reviewpicturerepository.AddReviewPicture(rewpicture);

        return addedRewPic;
    }

    public async Task<ReviewPictureDTO> DeleteReviewPictureByReviewIdAsync(int ReviewId)
    {
        var reviewpic = await _reviewpicturerepository.DeleteReviewPictureByReviewIdAsync(ReviewId);

        return _reviewpicmapper.MapToDTO(reviewpic) ?? null!;
    }

    public async Task<ICollection<ReviewPictureDTO>> GetAllReviewPicturesAsync(int PageSize, int PageNummer)
    {
        var reviewpics = await _reviewpicturerepository.GetAllReviewPicturesAsync(PageSize, PageNummer);
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
        var pic = await _reviewpicturerepository.GetReviewPictureByReviewIdAsync(ReviewId);
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
