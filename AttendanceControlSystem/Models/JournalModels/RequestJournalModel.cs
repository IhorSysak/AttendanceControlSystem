namespace AttendanceControlSystem.Models.JournalModels
{
    public class RequestJournalModel
    {
        public int Course { get; set; }
        public string Group { get; set; }
        public DateTime Date { get; set; }
        public string SubjectName { get; set; }
        public string TimeStart { get; set; }
    }
}
