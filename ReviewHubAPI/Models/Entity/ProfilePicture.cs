using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ReviewHubAPI.Models.Entity
{
    public class ProfilePicture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public byte[]? Picture { get; set; }


        public virtual User? User { get; set; }
    }
}
