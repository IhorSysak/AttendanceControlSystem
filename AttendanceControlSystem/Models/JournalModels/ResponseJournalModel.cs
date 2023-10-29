namespace AttendanceControlSystem.Models.JournalModels
{
    public class ResponseJournalModel
    {
        public int Course { get; set; }
        public string Group { get; set; }
        public string SubjectName { get; set; }
        public List<StudentPresenceInfo> StudentPresenceInfos { get; set; } = new List<StudentPresenceInfo>();
    }

    public class StudentPresenceInfo 
    {
        public int Position { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool IsPresent { get; set; }
    }
}
