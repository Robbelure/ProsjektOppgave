using System.ComponentModel.DataAnnotations;

namespace ReviewHubAPI.Models.DTO
{
    public class UserRegistrationDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;
    }
}
