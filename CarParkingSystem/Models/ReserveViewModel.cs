using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Models
{
    public class ReserveViewModel
    {
        public ParkingLotViewModel ParkingLotViewModel { get; set; }

        public UserViewModel UserViewModel { get; set; }

        [Required]
        public TimeOnly CheckInHour { get; set; }
        [Required]
        public TimeOnly CheckOutHour { get; set; }

        public string CardNumber { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public int CVV { get; set; }
    }
}
