using AttendanceControlSystem.Entity;
using System.Linq.Expressions;

namespace AttendanceControlSystem.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentAsync();
        Task<Student> GetStudentByParametetsAsync(Expression<Func<Student, bool>> filterExpression);
        Task<List<Student>> GetStudentsByParametetsAsync(Expression<Func<Student, bool>> filterExpression);
        Task CreateAsync(Student student);
        Task<Student> GetByIdAsync(string id);
        Task UpdateAsync(string id, Student student);
        Task RemoveAsync(string Id);
    }
}
