using System.ComponentModel.DataAnnotations;

namespace interview3core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; } // hashed password

        [Required]
        [MaxLength(50)]
        public required string Role { get; set; } // Admin or User
    }
}
