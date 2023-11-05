using System.ComponentModel.DataAnnotations;

namespace AttendanceControlSystem.Models.UserModels
{
    public class RegisterModel
    {
        [MinLength(2, ErrorMessage = "User name must be at least 2 characters")]
        [MaxLength(20, ErrorMessage = "User name cannot exceed 20 characters")]
        public string UserName { get; set; }
        [MinLength(3, ErrorMessage = "Password must be at least 3 characters")]
        [MaxLength(10, ErrorMessage = "Password cannot exceed 10 characters")]
        public string Password { get; set; }
        public required string Role { get; set; }
    }
}
