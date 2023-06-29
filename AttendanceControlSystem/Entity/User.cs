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
        public byte[] PasswordHash { get; set;}
        [BsonElement("passwordSalt")]
        public byte[] PasswordSalt { get; set; }
        [BsonElement("refreshToken")]
        public string RefreshToken { get; set; } = string.Empty;
        [BsonElement("tokenCreated")]
        public DateTime TokenCreated { get; set; }
        [BsonElement("tokenExpires")]
        public DateTime TokenExpires { get; set; }
        [BsonElement("role")]
        public string Role { get; set; }
    }
}
