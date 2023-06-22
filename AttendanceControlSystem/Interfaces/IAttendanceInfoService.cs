using AttendanceControlSystem.Entity;

namespace AttendanceControlSystem.Interfaces
{
    public interface IAttendanceInfoService
    {
        Task<List<AttendanceInfo>> GetAllAttendanceInfoAsync();
        Task CreateAsync(AttendanceInfo attendanceInfo);
        Task<AttendanceInfo> GetByIdAsync(string id);
        Task UpdateAsync(string id, AttendanceInfo attendanceInfo);
        Task RemoveAsync(string Id);
    }
}
