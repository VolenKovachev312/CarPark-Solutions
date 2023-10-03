using System.ComponentModel.DataAnnotations;
using static CarParkingSystem.Constants.Constants.User;

namespace CarParkingSystem.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 4)]
        public string Email { get; set; }

        [Required]
        [StringLength(FirstNameLengthMax, MinimumLength = FirstNameLengthMin)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(LastNameLengthMax,MinimumLength =LastNameLengthMin)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$",ErrorMessage ="Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
