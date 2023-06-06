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

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        public DateTime ReservationTime { get; set; } = DateTime.Now;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public StatusEnum Status { get; set; } = StatusEnum.Active;

        public virtual User User { get; set; }

        public virtual ParkingLot ParkingLot { get; set; }
    }
}
