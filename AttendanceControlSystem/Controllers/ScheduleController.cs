using AttendanceControlSystem.Interfaces;
using AttendanceControlSystem.Models.GroupModel;
using AttendanceControlSystem.Models.ScheduleModel;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Text.Json;

namespace AttendanceControlSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IAttendanceInfoService _attendanceInfoService;
        private readonly IStudentService _studentService;
        private readonly string URL = "https://schedule.kpi.ua/api/";

        public ScheduleController(IAttendanceInfoService attendanceInfoService, IStudentService studentService)
        {
            _attendanceInfoService = attendanceInfoService;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RequestScheduleModel requestScheduleModel)
        {
            HttpClient client = new HttpClient();

            var groupResponse = await client.GetStringAsync(URL + "schedule/groups");
            var groupsJsonData = JsonSerializer.Deserialize<GroupModel>(groupResponse);
            if (groupsJsonData == null)
                return NoContent();

            var groupId = groupsJsonData.Data.FirstOrDefault(x => x.Name == requestScheduleModel.Group).Id;
            Guid groupIdGuid = Guid.Empty;
            if(!Guid.TryParse(groupId, out groupIdGuid))
                throw new Exception($"There is group with such name '{requestScheduleModel.Group}'");

            var scheduleResponse = await client.GetStringAsync($"{URL}schedule/lessons?groupId={groupId}");
            var scheduleJsonData = JsonSerializer.Deserialize<ScheduleModule>(scheduleResponse);
            if (scheduleJsonData == null)
                return NoContent();

            /*var currentTimeResponse = await client.GetStringAsync(URL + "time/current");*/

            var isFirstEducationWeek = IsFirstWeek(requestScheduleModel.Date);
            ScheduleWeek searchedSchedule = default;

            if (isFirstEducationWeek)
            {
                searchedSchedule = scheduleJsonData.Data.ScheduleFirstWeek[(int)requestScheduleModel.Date.DayOfWeek - 1];
            }
            else 
            {
                searchedSchedule = scheduleJsonData.Data.ScheduleSecondWeek[(int)requestScheduleModel.Date.DayOfWeek - 1];
            }

            var student = await _studentService.GetStudentByParametetsAsync(i => i.FullName == requestScheduleModel.FullName && i.Group == requestScheduleModel.Group && i.Course == requestScheduleModel.Course);
            var (startDate, endTime) = CalculatingTimePeriod(requestScheduleModel.Date, searchedSchedule.Pairs.FirstOrDefault().Time);

            return Ok();
        }

        private static bool IsFirstWeek(DateTime checkDate) 
        {
            var currentDate = DateTime.Now;
            var september1st = new DateTime(currentDate.Year, 09, 01);
            var dayOfWeek = september1st.DayOfWeek;

            DateTime startEducation;
            if (dayOfWeek == DayOfWeek.Friday || dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
            {
                int daysUntilMonday = ((int)DayOfWeek.Monday - (int)dayOfWeek + 7) % 7;
                startEducation = september1st.AddDays(daysUntilMonday);
            }
            else
            {
                startEducation = september1st;
            }

            int count = 0;
            while (startEducation <= checkDate)
            {
                count++;
                startEducation = startEducation.AddDays(7);
            }
            
            return count % 2 != 0;
        }

        private static (DateTime startTime, DateTime endTime) CalculatingTimePeriod(DateTime inputTime, string time) 
        {
            var parts = time.Split('.');
            var hourses = int.Parse(parts[0]);
            var minutes = int.Parse(parts[1]);

            DateTime startDateTime = new DateTime(inputTime.Year, inputTime.Month, inputTime.Day, hourses, minutes, 0);

            var endDateTime = startDateTime.AddHours(1).AddMinutes(35);

            return (startDateTime, endDateTime);
        }
    }
}
