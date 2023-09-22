using System.ComponentModel.DataAnnotations;
using static CarParkingSystem.Constants.Constants.ParkingLot;
namespace CarParkingSystem.Models
{
    public class ParkingLotViewModel
    {
        [Required]
        [StringLength(CountryLengthMax,MinimumLength = CountryLengthMin)]
        public string Country { get; set; }

        [StringLength(StateLengthMax, MinimumLength = StateLengthMin)]
        public string? State { get; set; }

        [Required]
        [StringLength(CityLengthMax, MinimumLength=CityLengthMin)]
        public string City { get; set; }

        [Required]
        [Range(HourlyRateMin,HourlyRateMax)]
        public decimal HourlyRate { get; set; }
    }
}
