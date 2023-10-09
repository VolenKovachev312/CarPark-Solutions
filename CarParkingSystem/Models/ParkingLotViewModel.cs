using System.ComponentModel.DataAnnotations;
using static CarParkingSystem.Constants.Constants.ParkingLot;
using static CarParkingSystem.Constants.Constants.ErrorMessage;
namespace CarParkingSystem.Models
{
    public class ParkingLotViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(LocationLengthMax,MinimumLength =LocationLengthMin,ErrorMessage ="Name must be at least 2 characters long!")]
        public string Name { get; set; }

        [Required(ErrorMessage =RequiredErrorMessage)]
        public int Capacity { get; set; }

        public int AvailableSpaces { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(LatitudeMin, LatitudeMax, ErrorMessage ="Invalid latitude value! Must be between -90 and 90!")]
        public decimal Latitude { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(LongitudeMin, LongitudeMax, ErrorMessage = "Invalid longitude value! Must be between -180 and 180!")]
        public decimal Longitude { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Address { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public TimeOnly OpeningHour { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        public TimeOnly ClosingHour { get; set; }
        [Required]
        public bool IsNonStop { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Range(HourlyRateMin,HourlyRateMax, ErrorMessage ="Hourly Rate must be more than 0!")]
        public decimal HourlyRate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
