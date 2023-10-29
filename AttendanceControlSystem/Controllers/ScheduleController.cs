using AttendanceControlSystem.Interfaces;
using AttendanceControlSystem.Models.GroupModel;
using AttendanceControlSystem.Models.JournalModels;
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
        private readonly string URL = "https://api.campus.kpi.ua/";

        public ScheduleController(IAttendanceInfoService attendanceInfoService, IStudentService studentService)
        {
            _attendanceInfoService = attendanceInfoService;
            _studentService = studentService;
        }

        [HttpGet("GetSchedule")]
        public async Task<IActionResult> Get([FromQuery] RequestScheduleModel requestScheduleModel)
        {
            using var client = new HttpClient();

            var groupResponse = await client.GetStringAsync(URL + "schedule/groups");
            var groupsJsonData = JsonSerializer.Deserialize<GroupModel>(groupResponse);
            if (groupsJsonData == null)
                throw new Exception("No groups were found");

            string groupId = string.Empty;
            try
            {
                groupId = groupsJsonData.Data.FirstOrDefault(x => x.Name == requestScheduleModel.Group).Id;
            }
            catch
            {
                throw new Exception($"There is no group with such name '{requestScheduleModel.Group}'");
            }

            var student = await _studentService.GetStudentByParametetsAsync(i => i.FirstName == requestScheduleModel.FirstName && i.LastName == requestScheduleModel.LastName && i.MiddleName == requestScheduleModel.MiddleName && i.Group == requestScheduleModel.Group && i.Course == requestScheduleModel.Course);
            if (student == null)
                throw new Exception($"There is no student with the following parameters full name: '{requestScheduleModel.LastName} {requestScheduleModel.FirstName} {requestScheduleModel.MiddleName}' , group: '{requestScheduleModel.Group}' and course: '{requestScheduleModel.Course}'");
            var dayOfWeek = requestScheduleModel.Date.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Sunday)
                throw new Exception("There is no schedule in sunday");
            
            var scheduleResponse = await client.GetStringAsync($"{URL}schedule/lessons?groupId={groupId}");
            var scheduleJsonData = JsonSerializer.Deserialize<ScheduleModule>(scheduleResponse);
            if (scheduleJsonData == null)
                throw new Exception("No schedule were found");

            var isFirstEducationWeek = IsFirstWeek(requestScheduleModel.Date);
            ScheduleWeek searchedSchedule = default;

            if (isFirstEducationWeek)
            {
                searchedSchedule = scheduleJsonData.Data.ScheduleFirstWeek[(int)dayOfWeek - 1];
            }
            else
            {
                searchedSchedule = scheduleJsonData.Data.ScheduleSecondWeek[(int)dayOfWeek - 1];
            }

            var scheduleData = new ResponseScheduleModel
            {
                DayOfWeek = searchedSchedule.Day
            };

            foreach (var pair in searchedSchedule.Pairs.OrderBy(x => x.Time))
            {
                var (startTime, endTime) = CalculatingTimePeriod(requestScheduleModel.Date, pair.Time);
                var attendanceInfos = await _attendanceInfoService.GetAttendanceInfoByParametetsAsync(i => i.Time >= startTime && i.Time <= endTime && i.Student.Id == student.Id);

                TimeSpan totalTimeInClass = TimeSpan.Zero;

                if (attendanceInfos.Count != 0) 
                {
                    var timestamps = attendanceInfos.Select(info => info.Time).Order().ToList();

                    var fistTimeStamp = timestamps.FirstOrDefault();
                    var lastTimeStamp = timestamps.LastOrDefault();

                    if (fistTimeStamp < startTime)
                        fistTimeStamp = startTime;

                    if (lastTimeStamp > endTime)
                        lastTimeStamp = endTime;

                    totalTimeInClass += lastTimeStamp - fistTimeStamp;
                }

                var scheduleInfo = new ScheduleInfo
                {
                    IsPresent = attendanceInfos.Count != 0,
                    Subject = pair,
                    Snapshots = attendanceInfos,
                    TotalTimePresence = totalTimeInClass
                };

                scheduleData.ScheduleInfos.Add(scheduleInfo);
            }

            scheduleData.ScheduleInfos = scheduleData.ScheduleInfos.OrderBy(info => DateTime.ParseExact(info.Subject.Time, "H.mm" , null)).ToList();

            return Ok(scheduleData);
        }

        [HttpGet("GetSubject")]
        public async Task<IActionResult> Get([FromQuery] RequestSubjectModel requestSubjectModel)
        {
            var dayOfWeek = requestSubjectModel.Date.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Sunday)
                throw new Exception("There is no schedule in sunday");

            using var client = new HttpClient();

            var groupResponse = await client.GetStringAsync(URL + "schedule/groups");
            var groupsJsonData = JsonSerializer.Deserialize<GroupModel>(groupResponse);
            if (groupsJsonData == null)
                throw new Exception("No groups were found");

            string groupId = string.Empty;
            try
            {
                groupId = groupsJsonData.Data.FirstOrDefault(x => x.Name == requestSubjectModel.Group).Id;
            }
            catch 
            {
                throw new Exception($"There is no group with such name '{requestSubjectModel.Group}'");
            }

            var scheduleResponse = await client.GetStringAsync($"{URL}schedule/lessons?groupId={groupId}");
            var scheduleJsonData = JsonSerializer.Deserialize<ScheduleModule>(scheduleResponse);
            if (scheduleJsonData == null)
                throw new Exception("No schedule were found");

            var isFirstEducationWeek = IsFirstWeek(requestSubjectModel.Date);
            ScheduleWeek searchedSchedule = default;

            if (isFirstEducationWeek)
            {
                searchedSchedule = scheduleJsonData.Data.ScheduleFirstWeek[(int)dayOfWeek - 1];
            }
            else
            {
                searchedSchedule = scheduleJsonData.Data.ScheduleSecondWeek[(int)dayOfWeek - 1];
            }

            var subjectsData = searchedSchedule.Pairs.Select(subject => new ResponseSubjectModel
            {
                SubjectName = $"{subject.Name} - {subject.Tag}",
                TimeStart = subject.Time
            });

            return Ok(subjectsData);
        }

        [HttpGet("GetJournal")]
        public async Task<IActionResult> Get([FromQuery] RequestJournalModel requestJournalModel)
        {
            var students = await _studentService.GetStudentsByParametetsAsync(i => i.Group == requestJournalModel.Group && i.Course == requestJournalModel.Course);
            if (students == null)
                throw new Exception($"There is no students with the following parameters course: '{requestJournalModel.Course}' and group: '{requestJournalModel.Group}'");

            var (startTime, endTime) = CalculatingTimePeriod(requestJournalModel.Date, requestJournalModel.TimeStart);

            var responseJournalModel = new ResponseJournalModel
            {
                Course = requestJournalModel.Course,
                Group = requestJournalModel.Group,
                SubjectName = requestJournalModel.SubjectName
            };

            int position = 1;
            foreach (var student in students.OrderBy(x => x.LastName)) 
            {
                var attendanceInfos = await _attendanceInfoService.GetAttendanceInfoByParametetsAsync(i => i.Time >= startTime && i.Time <= endTime && i.Student.Id == student.Id);

                var studentPresenceInfo = new StudentPresenceInfo
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    MiddleName = student.MiddleName,
                    IsPresent = attendanceInfos.Count != 0,
                    Position = position++
                };

                responseJournalModel.StudentPresenceInfos.Add(studentPresenceInfo);
            }

            return Ok(responseJournalModel);
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

            var startDateTime = new DateTime(inputTime.Year, inputTime.Month, inputTime.Day, hourses, minutes, 0, DateTimeKind.Utc);

            var endDateTime = startDateTime.AddHours(1).AddMinutes(35);

            return (startDateTime, endDateTime);
        }
    }
}
