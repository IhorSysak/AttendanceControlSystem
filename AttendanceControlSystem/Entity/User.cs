using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AttendanceControlSystem.Entity
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        [BsonElement("userName")]
        public string UserName { get; set; } 
        [BsonElement("passwordHash")]
        public string PasswordHash { get; set;}
        [BsonElement("role")]
        public string Role { get; set; }
    }
}
