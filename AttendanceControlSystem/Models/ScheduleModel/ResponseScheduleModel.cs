using AttendanceControlSystem.Entity;

namespace AttendanceControlSystem.Models.ScheduleModel
{
    public class ResponseScheduleModel
    {
        public string DayOfWeek { get; set; }
        public List<ScheduleInfo> ScheduleInfos { get; set; } = new List<ScheduleInfo>();
    }

    public class ScheduleInfo 
    {
        public bool IsPresent { get; set; }
        public Pair Subject { get; set; }
        public List<AttendanceInfo> Snapshots { get; set; }
        public TimeSpan TotalTimePresence { get; set; }
    }
}
