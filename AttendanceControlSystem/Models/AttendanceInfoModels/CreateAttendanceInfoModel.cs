using AttendanceControlSystem.Entity;

namespace AttendanceControlSystem.Models.AttendanceInfoModels
{
    public class CreateAttendanceInfoModel
    {
        public DateTime Time { get; set; }
        public Student Student { get; set; }
    }
}
