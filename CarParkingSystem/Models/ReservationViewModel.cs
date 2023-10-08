namespace CarParkingSystem.Models
{
    public class ReservationViewModel
    {
        public Guid ReservationId { get; set; }
        public string ParkingName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsCancelled { get; set; }
        public string LicensePlateNumber { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }

    }
}
