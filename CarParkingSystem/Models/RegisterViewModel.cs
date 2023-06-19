using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 4)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
