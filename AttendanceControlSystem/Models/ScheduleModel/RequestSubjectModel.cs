using System.ComponentModel.DataAnnotations;

namespace AttendanceControlSystem.Models.ScheduleModel
{
    public class RequestSubjectModel
    {
        [MinLength(3, ErrorMessage = "Group must be at least 3 characters")]
        [MaxLength(10, ErrorMessage = "Group cannot exceed 10 characters")]
        public string Group { get; set; }
        public DateTime Date { get; set; }
    }
}
