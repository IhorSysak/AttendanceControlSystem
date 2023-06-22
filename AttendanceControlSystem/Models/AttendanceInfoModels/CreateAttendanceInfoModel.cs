using AttendanceControlSystem.Entity;

namespace AttendanceControlSystem.Models.AttendanceInfoModels
{
    public class CreateAttendanceInfoModel
    {
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public Student Student { get; set; }
    }
}
