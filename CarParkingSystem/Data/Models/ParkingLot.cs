using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarParkingSystem.Constants.Constants.ParkingLot;

namespace CarParkingSystem.Data.Models
{
    public class ParkingLot
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }    

        [Required]
        [Range(LatitudeMin,LatitudeMax)]
        [Column(TypeName ="decimal(18,9)")]
        public decimal Latitude { get; set; }

        [Required]
        [Range(LongitudeMin,LongitudeMax)]
        [Column(TypeName = "decimal(18,9)")]
        public decimal Longitude { get;set; }

        [Required]
        public string Address { get; set; }

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
        [Column(TypeName="TIME(7)")]
        public TimeOnly OpeningHour { get; set; }

        [Required]
        [Column(TypeName = "TIME(7)")]
        public TimeOnly ClosingHour { get; set; }

        [Required]
        public bool IsNonStop { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
        //Concurrency check
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
