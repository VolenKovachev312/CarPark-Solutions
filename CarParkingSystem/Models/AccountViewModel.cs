using System.ComponentModel.DataAnnotations;
using static CarParkingSystem.Constants.Constants.ErrorMessage;
namespace CarParkingSystem.Models
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string? OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string? ConfirmNewPassword { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string? LicensePlateNumber { get; set; }
    }
}
