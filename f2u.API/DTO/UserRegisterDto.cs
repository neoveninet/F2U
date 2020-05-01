using System.ComponentModel.DataAnnotations;

namespace f2u.API.DTO
{
    public class UserRegisterDto
    {

        [Required]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "The username must be between 4 and 10 characters.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "The password must be between 8 and 16 characters.")]
        public string Password { get; set; }
    }
}