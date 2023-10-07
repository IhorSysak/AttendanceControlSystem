using AttendanceControlSystem.Models.GroupModel;
using AttendanceControlSystem.Models.ScheduleModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AttendanceControlSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly string URL = "https://schedule.kpi.ua/api/";

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

            var currentTimeResponse = await client.GetStringAsync(URL + "time/current");

            return Ok();
        }
    }
}
