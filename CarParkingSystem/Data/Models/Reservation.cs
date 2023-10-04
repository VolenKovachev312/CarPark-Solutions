using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarParkingSystem.Data.Models
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(ParkingLot))]
        public Guid ParkingLotId { get; set; }
        public virtual ParkingLot ParkingLot { get; set; }


        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime ReservationTime { get; set; } = DateTime.Now;

        [Required]
        public DateTime CheckInTime { get; set; }

        [Required]
        public DateTime CheckOutTime { get; set; }

        [Required]
        public StatusEnum Status { get; set; } = StatusEnum.Active;


    }
}
