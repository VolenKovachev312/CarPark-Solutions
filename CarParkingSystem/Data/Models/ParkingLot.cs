using System.ComponentModel.DataAnnotations;
using static CarParkingSystem.Constants.Constants.ParkingLot;

namespace CarParkingSystem.Data.Models
{
    public class ParkingLot
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(LocationLengthMax,MinimumLength =LocationLengthMin)]
        public string Location { get; set; }

        [Required]
        public decimal x { get; set; }

        [Required] 
        public decimal y { get;set; }

        [Required]
        [Range(CapacityMin,CapacityMax)]
        public int Capacity { get; set; }

        [Required]
        [Range(0, CapacityMax)]
        public int AvailableSpaces { get; set; }

        [Required]
        [Range(HourlyRateMin,HourlyRateMax)]
        public decimal HourlyRate { get; set; }

        [Required]
        [Url]
        public string ImageURL { get; set; }

        //Concurrency check
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
