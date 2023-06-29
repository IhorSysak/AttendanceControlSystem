using AttendanceControlSystem.Entity;

namespace AttendanceControlSystem.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetByUserNameAsync(string userName);
        Task CreateAsync(User user);
        Task UpdateAsync(string id, User user);
        Task RemoveAsync(string Id);
        public string GetUser();
    }
}
