using System.ComponentModel.DataAnnotations;

namespace VehicleServiceCenter.Application.DTOs.Users
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "User type is required")]
        public string UserType { get; set; } // Admin, Customer, Technician
    }
}
