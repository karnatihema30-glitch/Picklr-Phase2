using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Picklr.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        public int ProgramID { get; set; }

        [ForeignKey("ProgramID")]
        public PicklProgram? Program { get; set; }

        [Required]
        public string ClubName { get; set; } = string.Empty;

        [Required]
        public string ReservationDate { get; set; } = string.Empty;

        public decimal Fee { get; set; }

        public DateTime ConfirmedOn { get; set; } = DateTime.Now;
    }
}