using System.ComponentModel.DataAnnotations;
using static CarParkingSystem.Constants.Constants.ParkingLot;
namespace CarParkingSystem.Models
{
    public class ReserveViewModel
    {
        public ParkingLotViewModel ParkingLotViewModel { get; set; }

        public UserViewModel UserViewModel { get; set; }


        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy hh:mm A}")]
        public DateTime CheckInHour { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy hh:mm A}")]

        public DateTime CheckOutHour { get; set; }

        [Required]
        [Range(0,double.MaxValue,ErrorMessage ="Enter valid dates!")]
        public decimal Price { get; set; }

        public string? CardNumber { get; set; }

        public int? ExpirationMonth { get; set; }

        public int? ExpirationYear { get; set; }

        public int? CVV { get; set; }
    }
}
