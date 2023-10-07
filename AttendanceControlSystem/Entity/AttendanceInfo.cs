using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AttendanceControlSystem.Entity
{
    public class AttendanceInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        [BsonElement("time")]
        public DateTime Time { get; set; }
        [BsonElement("student")]
        public Student Student { get; set; }
    }
}
