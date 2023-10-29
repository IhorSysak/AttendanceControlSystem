using System.ComponentModel.DataAnnotations;

namespace AttendanceControlSystem.Models.GroupModel
{
    public class RequestScheduleModel
    {
        [Range(1, 6, ErrorMessage = "Course must be between 1 and 6")]
        public int Course { get; set; }
        
        [MinLength(3, ErrorMessage = "Group must be at least 3 characters")]
        [MaxLength(10, ErrorMessage = "Group cannot exceed 10 characters")]
        public string Group { get; set; }
        
        public DateTime Date { get; set; }

        [MinLength(2, ErrorMessage = "First name must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; }
        [MinLength(2, ErrorMessage = "Middle name must be at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Middle name cannot exceed 50 characters")]
        public string MiddleName { get; set; }
    }
}
