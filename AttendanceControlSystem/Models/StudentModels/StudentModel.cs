using System.ComponentModel.DataAnnotations;

namespace AttendanceControlSystem.Models.StudentModels
{
    public class StudentModel
    {
        public string Id { get; set; }

        [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; }
        [MinLength(2, ErrorMessage = "Middle name must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Middle name cannot exceed 50 characters")]
        public string MiddleName { get; set; }
        [Range(1, 6, ErrorMessage = "Course must be between 1 and 6")]
        public int Course { get; set; }
        [MinLength(3, ErrorMessage = "Group must be at least 3 characters")]
        [MaxLength(10, ErrorMessage = "Group cannot exceed 10 characters")]
        public string Group { get; set; }
        [RegularExpression(@"[\w_.%+-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Not valid email address")]
        public string Email { get; set; }
        public string ImagePath { get; set; }
    }
}
