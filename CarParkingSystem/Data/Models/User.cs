using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static CarParkingSystem.Constants.Constants.User;

namespace CarParkingSystem.Data.Models
{
    public class User:IdentityUser<Guid>
    {
        public string? LicensePlateNumber { get; set; }

        [Required]
        [StringLength(FirstNameLengthMax, MinimumLength = FirstNameLengthMin)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameLengthMax, MinimumLength = LastNameLengthMin)]
        public string LastName { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
