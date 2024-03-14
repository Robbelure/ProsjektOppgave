using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.DTO
{
    public class MovieDTO
    {
   
        public int Id { get; set; }
  
        public string MovieName { get; set; } = string.Empty;
       
        public int ReleaseYear { get; set; }
    
        public string Director { get; set; } = string.Empty;
        
        public string Genre { get; set; } = string.Empty;
        
        public DateTime DateCreated { get; set; }
        
        public DateTime DateUpdated { get; set; }
    }
}
