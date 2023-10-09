using System.ComponentModel.DataAnnotations;
using static CarParkingSystem.Constants.Constants.ErrorMessage;
namespace CarParkingSystem.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
