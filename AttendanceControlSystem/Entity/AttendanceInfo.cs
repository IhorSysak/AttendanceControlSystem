using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AttendanceControlSystem.Entity
{
    public class AttendanceInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        [BsonElement("timeIn")]
        public DateTime TimeIn { get; set; }
        [BsonElement("timeOut")]
        public DateTime TimeOut { get; set; }
        [BsonElement("student")]
        public Student Student { get; set; }
    }
}
