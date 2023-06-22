using AttendanceControlSystem.Interfaces;

namespace AttendanceControlSystem.MongoDBSettings
{
    public class MongoSettings : IMongoSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
