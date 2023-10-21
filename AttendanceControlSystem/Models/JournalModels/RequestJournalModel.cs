namespace AttendanceControlSystem.Models.JournalModels
{
    public class RequestJournalModel
    {
        public int Course { get; set; }
        public string Group { get; set; }
        public DateTime Date { get; set; }
        public Subject Subject { get; set; } 
    }

    public class Subject 
    {
        public string Name { get; set; }
        public string Time { get; set; }
    }
}
