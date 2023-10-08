using AttendanceControlSystem.Entity;
using System.Linq.Expressions;

namespace AttendanceControlSystem.Interfaces
{
    public interface IAttendanceInfoService
    {
        Task<List<AttendanceInfo>> GetAllAttendanceInfoAsync();
        Task CreateAsync(AttendanceInfo attendanceInfo);
        Task<AttendanceInfo> GetAttendanceInfoByParametetsAsync(Expression<Func<AttendanceInfo, bool>> filterExpression);
        Task<AttendanceInfo> GetByIdAsync(string id);
        Task UpdateAsync(string id, AttendanceInfo attendanceInfo);
        Task RemoveAsync(string Id);
    }
}
