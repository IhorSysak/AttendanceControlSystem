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
        [BsonElement("firstName")]
        public string FirstName { get; set; } = null!;
        [BsonElement("lastName")]
        public string LastName { get; set; } = null!;
        [BsonElement("middleName")]
        public string MiddleName { get; set; } = null!;
        [BsonElement("course")]
        public int Course { get; set; }
        [BsonElement("group")]
        public string Group { get; set; } = null!;
        [BsonElement("email")]
        public string Email { get; set; } = null!;
        [BsonElement("ImagePath")]
        public string ImagePath { get; set; } = null!;
    }
}
