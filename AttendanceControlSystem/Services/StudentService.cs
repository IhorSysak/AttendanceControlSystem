using AttendanceControlSystem.Entity;
using AttendanceControlSystem.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace AttendanceControlSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMongoCollection<Student> _student;

        public StudentService(IConfiguration configuration) 
        {
            var mongoClient = new MongoClient(configuration.GetValue<string>("MongoDB:ConnectionURI"));
            var dataBase = mongoClient.GetDatabase(configuration.GetValue<string>("MongoDB:DataBaseName"));
            _student = dataBase.GetCollection<Student>("Student");
        }

        public async Task<List<Student>> GetAllStudentAsync() =>
            await _student.Find(new BsonDocument()).ToListAsync();

        public async Task<Student> GetStudentByParametetsAsync(Expression<Func<Student, bool>> filterExpression) =>
            await _student.Find(filterExpression).FirstOrDefaultAsync();

        public async Task CreateAsync(Student student) =>
            await _student.InsertOneAsync(student);

        public async Task<Student> GetByIdAsync(string id) =>
            await _student.Find(s => s.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, Student student) =>
            await _student.ReplaceOneAsync(s => s.Id == id, student);

        public async Task RemoveAsync(string Id) =>
            await _student.DeleteOneAsync(s => s.Id == Id);
    }
}
