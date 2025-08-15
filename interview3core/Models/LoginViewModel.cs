using System.ComponentModel.DataAnnotations;

namespace interview3core.Models
{
    public class LoginViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(50)]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
