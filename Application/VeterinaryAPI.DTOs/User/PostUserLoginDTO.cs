using System.ComponentModel.DataAnnotations;

namespace VeterinaryAPI.DTOs.User
{
    public class PostUserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
