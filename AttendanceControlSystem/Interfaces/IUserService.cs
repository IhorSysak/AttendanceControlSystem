using AttendanceControlSystem.Entity;

namespace AttendanceControlSystem.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetByUserNameAsync(string userName);
        Task CreateAsync(User user);
        Task RemoveAsync(string Id);
    }
}
