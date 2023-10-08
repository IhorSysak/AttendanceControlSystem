using AttendanceControlSystem.Entity;
using AttendanceControlSystem.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace AttendanceControlSystem.Services
{
    public class AttendanceInfoService : IAttendanceInfoService
    {
        private readonly IMongoCollection<AttendanceInfo> _attendanceInfo;

        public AttendanceInfoService(IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration.GetValue<string>("MongoDB:ConnectionURI"));
            var dataBase = mongoClient.GetDatabase(configuration.GetValue<string>("MongoDB:DataBaseName"));
            _attendanceInfo = dataBase.GetCollection<AttendanceInfo>("AttendanceInfo");
        }

        public async Task<List<AttendanceInfo>> GetAllAttendanceInfoAsync() =>
            await _attendanceInfo.Find(new BsonDocument()).ToListAsync();

        public async Task<AttendanceInfo> GetAttendanceInfoByParametetsAsync(Expression<Func<AttendanceInfo, bool>> filterExpression) =>
            await _attendanceInfo.Find(filterExpression).FirstOrDefaultAsync();

        public async Task CreateAsync(AttendanceInfo attendanceInfo) =>
            await _attendanceInfo.InsertOneAsync(attendanceInfo);

        public async Task<AttendanceInfo> GetByIdAsync(string id) =>
            await _attendanceInfo.Find(s => s.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, AttendanceInfo attendanceInfo) =>
            await _attendanceInfo.ReplaceOneAsync(s => s.Id == id, attendanceInfo);

        public async Task RemoveAsync(string Id) =>
            await _attendanceInfo.DeleteOneAsync(s => s.Id == Id);
    }
}
