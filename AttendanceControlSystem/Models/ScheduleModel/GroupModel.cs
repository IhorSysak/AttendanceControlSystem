using System.Text.Json.Serialization;

namespace AttendanceControlSystem.Models.GroupModel
{
    public class GroupModel
    {
        [JsonPropertyName("data")]
        public required List<Group> Data { get; set; }
    }

    public class Group 
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("faculty")]
        public string? Faculty { get; set; }
    }
}
