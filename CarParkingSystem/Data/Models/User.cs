using Microsoft.AspNetCore.Identity;

namespace CarParkingSystem.Data.Models
{
    public class User:IdentityUser<Guid>
    {
        public string? CarNumber { get; set; }

        public string? CarMake { get; set; }

        public string? CarModel { get; set; }

        public decimal Balance { get; set; } = 50;
    }
}
