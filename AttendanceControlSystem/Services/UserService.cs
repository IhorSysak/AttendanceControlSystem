using AttendanceControlSystem.Entity;
using AttendanceControlSystem.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AttendanceControlSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration.GetValue<string>("MongoDB:ConnectionURI"));
            var dataBase = mongoClient.GetDatabase(configuration.GetValue<string>("MongoDB:DataBaseName"));
            _user = dataBase.GetCollection<User>("User");

            var uniqueIndex = Builders<User>.IndexKeys.Ascending(x => x.UserName);
            var options = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<User>(uniqueIndex, options);
            _user.Indexes.CreateOne(indexModel);
        }

        public async Task<List<User>> GetAllUsersAsync() =>
            await _user.Find(new BsonDocument()).ToListAsync();

        public async Task<User> GetByUserNameAsync(string userName) =>
            await _user.Find(s => s.UserName == userName).FirstOrDefaultAsync();

        public async Task CreateAsync(User user) =>
            await _user.InsertOneAsync(user);

        public async Task RemoveAsync(string Id) =>
            await _user.DeleteOneAsync(s => s.Id == Id);
    }
}
