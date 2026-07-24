using System.ComponentModel.DataAnnotations;

namespace Picklr.Models
{
    public class Club
    {
        [Key]
        public int ClubID { get; set; }

        [Required(ErrorMessage = "Please enter a club name.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a location.")]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}