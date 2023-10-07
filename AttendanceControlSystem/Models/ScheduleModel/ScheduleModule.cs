using System.Text.Json.Serialization;

namespace AttendanceControlSystem.Models.ScheduleModel
{
    public class ScheduleModule
    {
        [JsonPropertyName("data")]
        public required Data Data { get; set; }
    }

    public class Data 
    {
        [JsonPropertyName("groupCode")]
        public required string GroupCode { get; set; }
        [JsonPropertyName("scheduleFirstWeek")]
        public required List<ScheduleFirstWeek> ScheduleFirstWeek { get; set; }
        [JsonPropertyName("scheduleSecondWeek")]
        public required List<ScheduleFirstWeek> ScheduleSecondWeek { get; set; }
    }

    public class ScheduleFirstWeek 
    {
        [JsonPropertyName("day")]
        public required string Day { get; set; }
        [JsonPropertyName("pairs")]
        public required List<Pair> Pairs { get; set; }
    }

    public class ScheduleSecondWeek
    {
        [JsonPropertyName("day")]
        public required string Day { get; set; }
        [JsonPropertyName("pairs")]
        public required List<Pair> Pairs { get; set; }
    }

    public class Pair 
    {
        [JsonPropertyName("teacherName")]
        public string TeacherName { get; set; }
        [JsonPropertyName("lecturerId")]
        public string LecturerId { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("place")]
        public string Place { get; set; }
        [JsonPropertyName("tag")]
        public string Tag { get; set; }
    }
}
