using System.ComponentModel.DataAnnotations;
using static CarParkingSystem.Constants.Constants.ParkingLot;
namespace CarParkingSystem.Models
{
    public class ParkingLotViewModel
    {
        //[Required]
        //[StringLength(CountryLengthMax,MinimumLength = CountryLengthMin)]
        //public string Country { get; set; }

        //[StringLength(StateLengthMax, MinimumLength = StateLengthMin)]
        //public string? State { get; set; }

        //[Required]
        //[StringLength(CityLengthMax, MinimumLength=CityLengthMin)]
        //public string City { get; set; }

        [Required]
        [StringLength(LocationLengthMax,MinimumLength =4,ErrorMessage ="Name must be at least 4 characters long!")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Required!")]
        public int Capacity { get; set; }

        public int AvailableSpaces { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        [Required] 
        public decimal Longitude { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public TimeOnly OpeningHour { get; set; }
        [Required]
        public TimeOnly ClosingHour { get; set; }
        [Required]
        public bool IsNonStop { get; set; }
        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Range(HourlyRateMin,HourlyRateMax, ErrorMessage ="Hourly Rate must be more than 0!")]
        public decimal HourlyRate { get; set; } 
    }
}
