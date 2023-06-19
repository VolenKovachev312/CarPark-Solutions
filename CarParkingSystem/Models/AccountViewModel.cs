using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Models
{
    public class AccountViewModel
    {
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        public string? ConfirmNewPassword { get; set; }


        public string? CarModel { get; set; }

        public string? CarMake { get; set; }

        public string? CarNumber { get; set; }
    }
}
