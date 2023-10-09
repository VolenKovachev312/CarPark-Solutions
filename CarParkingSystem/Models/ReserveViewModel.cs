using System.ComponentModel.DataAnnotations;
using static CarParkingSystem.Constants.Constants.ErrorMessage;
namespace CarParkingSystem.Models
{
    public class ReserveViewModel
    {
        public ParkingLotViewModel ParkingLotViewModel { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy hh:mm A}")]
        [Range(typeof(DateTime), "1/1/2011", "1/1/2999", ErrorMessage = "Date is out of Range")]
        public DateTime CheckInHour { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy hh:mm A}")]
        [Range(typeof(DateTime), "1/1/2011", "1/1/2999", ErrorMessage = "Date is out of Range")]
        public DateTime CheckOutHour { get; set; }

        [Required]
        [Range(0,double.MaxValue,ErrorMessage ="Enter valid dates!")]
        public decimal Price { get; set; }

        public Guid? UserId { get; set; }

        [Required(ErrorMessage =RequiredErrorMessage)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string LastName { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string LicensePlateNumber { get; set; }

        public string? CardNumber { get; set; }

        public int? ExpirationMonth { get; set; }

        public int? ExpirationYear { get; set; }

        public int? CVV { get; set; }
    }
}
