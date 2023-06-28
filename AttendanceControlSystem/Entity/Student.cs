using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AttendanceControlSystem.Entity
{
    [BsonIgnoreExtraElements]
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();
        [BsonElement("fullName")]
        public string FullName { get; set; } = null!;
        [BsonElement("course")]
        public int Course { get; set; }
        [BsonElement("group")]
        public string Group { get; set; } = null!;
        [BsonElement("ImagePath")]
        public string ImagePath { get; set; } = null!;
    }
}
