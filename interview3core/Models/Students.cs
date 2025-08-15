using System.ComponentModel.DataAnnotations;

namespace interview3core.Models
{
    public class Students
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required]
        [StringLength(100)]
        public required string FatherName { get; set; }
        [Required]
        [StringLength(100)]
        public required string MotherName { get; set; }
    }
}
