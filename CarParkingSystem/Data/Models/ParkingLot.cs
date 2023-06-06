using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Data.Models
{
    public class ParkingLot
    {
        [Key]
        public Guid Id { get; set; }

        public string Location { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public int AvailableSpaces { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

    }
}
