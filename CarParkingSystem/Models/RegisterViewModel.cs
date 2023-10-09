using System.ComponentModel.DataAnnotations;
using static CarParkingSystem.Constants.Constants.User;
using static CarParkingSystem.Constants.Constants.ErrorMessage;
namespace CarParkingSystem.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage =RequiredErrorMessage)]
        [EmailAddress]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Email should be between 4 and 100 characters!")]
        public string Email { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(FirstNameLengthMax, MinimumLength = FirstNameLengthMin, ErrorMessage = "First Name should be between 2 and 20 characters!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(LastNameLengthMax,MinimumLength =LastNameLengthMin,ErrorMessage ="Last Name should be between 2 and 20 characters!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$",ErrorMessage ="Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(20, MinimumLength = 6,ErrorMessage ="Password length should be between 6 and 20 characters!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password),ErrorMessage ="Input does not match password!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
