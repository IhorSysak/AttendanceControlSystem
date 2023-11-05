using AttendanceControlSystem.Interfaces;
using AttendanceControlSystem.Models.TeacherModel;
using AttendanceControlSystem.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceControlSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleConstants.Admin)]
    public class TeacherController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public TeacherController(IUserService userService, IMapper mapper) 
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<TeacherModel>> GetAll() 
        {
            var users = await _userService.GetAllUsersAsync();
            var teachers = users.Where(x => x.Role == RoleConstants.Teacher).ToList();

            List<TeacherModel> teachersMapped = new List<TeacherModel>();
            foreach (var teacher in teachers) 
            {
                var result = _mapper.Map<TeacherModel>(teacher);
                teachersMapped.Add(result);
            }

            return teachersMapped;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _userService.RemoveAsync(id);
            return NoContent();
        }
    }
}
