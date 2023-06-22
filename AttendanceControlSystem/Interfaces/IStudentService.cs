using AttendanceControlSystem.Entity;

namespace AttendanceControlSystem.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentAsync();
        Task CreateAsync(Student student);
        Task<Student> GetByIdAsync(string id);
        Task UpdateAsync(string id, Student student);
        Task RemoveAsync(string Id);
    }
}
