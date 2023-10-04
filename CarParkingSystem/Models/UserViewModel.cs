using CarParkingSystem.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Models
{
    public class UserViewModel
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string LicensePlateNumber { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
