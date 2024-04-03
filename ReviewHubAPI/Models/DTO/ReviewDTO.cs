

namespace ReviewHubAPI.Models.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        
        public int MovieId { get; set; }
        
        public int Userid { get; set; }
      
        public string Title { get; set; } = string.Empty;
                                                                
        public int Rating { get; set; }
        
        public string Text { get; set; } = string.Empty;
       
    }
}
