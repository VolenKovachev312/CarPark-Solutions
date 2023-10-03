using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Models
{
    public class AccountViewModel
    {
        [Required]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string? OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string? ConfirmNewPassword { get; set; }

        [Required]
        public string? LicensePlateNumber { get; set; }
    }
}
