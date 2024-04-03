using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewHubAPI.Models.Entity
{
    public class ProfilePictureEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UserID")]
        public int UserID { get; set; }

        public byte[]? ProfilePicture { get; set; }


        public virtual UserEntity? UserEntity { get; set; }
    }
}
