namespace AttendanceControlSystem.Interfaces
{
    public interface IMongoSettings
    {
        public string ConnectionURI { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
