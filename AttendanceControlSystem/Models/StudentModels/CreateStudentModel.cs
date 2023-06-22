using System.ComponentModel.DataAnnotations;

namespace AttendanceControlSystem.Models.StudentModels
{
    public class CreateStudentModel
    {
        [MinLength(2, ErrorMessage = "Full name must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Full name cannot exceed 50 characters")]
        public string FullName { get; set; }
        [Range(1, 6, ErrorMessage = "Course must be between 1 and 6")]
        public int Course { get; set; }
        [MinLength(3, ErrorMessage = "Group must be at least 3 characters")]
        [MaxLength(10, ErrorMessage = "Group cannot exceed 10 characters")]
        public string Group { get; set; }
        public IFormFile File { get; set; }
    }
}
