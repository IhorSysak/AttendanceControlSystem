namespace AttendanceControlSystem.Models.AttendanceInfoModels
{
    public class SearchAttendanceInfoModel
    {
        public string StudentId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
    }
}
