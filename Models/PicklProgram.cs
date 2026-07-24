using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Picklr.Models
{
    // Named PicklProgram to avoid conflict with Program.cs
    public class PicklProgram
    {
        [Key]
        public int ProgramID { get; set; }

        [Required(ErrorMessage = "Please enter a program name.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public string AvailableDays { get; set; } = string.Empty;

        [Range(0, 10000, ErrorMessage = "Fee must be between 0 and 10000.")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Fee { get; set; }

        [Required(ErrorMessage = "Please select a club.")]
        public int ClubID { get; set; }

        [ForeignKey("ClubID")]
        public virtual Club? Club { get; set; }
    }
}