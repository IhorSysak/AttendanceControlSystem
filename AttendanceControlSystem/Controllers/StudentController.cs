using AttendanceControlSystem.Entity;
using AttendanceControlSystem.Interfaces;
using AttendanceControlSystem.Models.StudentModels;
using AttendanceControlSystem.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceControlSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConstants.Admin}, {RoleConstants.Teacher}, {RoleConstants.Student}")]
        public async Task<List<Student>> GetAllAsync() =>
            await _studentService.GetAllStudentAsync();

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RoleConstants.Admin}, {RoleConstants.Teacher}")]
        public async Task<IActionResult> GetAsync([FromRoute] string id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound($"There is no student with such id '{id}'");

            return Ok(student);
        }

        [HttpGet("GetStundetByParameters")]
        [Authorize(Roles = $"{RoleConstants.Admin}, {RoleConstants.Teacher}")]
        public async Task<IActionResult> FindStudentByParametesAsync([FromQuery] SearchStudentModel searchStudentModel)
        {
            var student = await _studentService.GetStudentByParametetsAsync(i => i.FirstName == searchStudentModel.FirstName && i.LastName == searchStudentModel.LastName && i.MiddleName == searchStudentModel.MiddleName && i.Group == searchStudentModel.Group && i.Course == searchStudentModel.Course);
            if (student == null)
                throw new Exception("There is no student with such parameters");

            return Ok(student);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Admin}, {RoleConstants.Teacher}")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateStudentModel createStudentModel)
        {
            var student = _mapper.Map<Student>(createStudentModel);

            await _studentService.CreateAsync(student);
            return Ok(student);
        }

        [HttpPut]
        [Authorize(Roles = $"{RoleConstants.Admin}, {RoleConstants.Teacher}")]
        public async Task<IActionResult> UpdateAsync([FromBody] StudentModel studentModel)
        {
            var student = _mapper.Map<Student>(studentModel);
            await _studentService.UpdateAsync(studentModel.Id, student);
            return Ok(student);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RoleConstants.Admin}, {RoleConstants.Teacher}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound($"There is no student with such id '{id}'");

            await _studentService.RemoveAsync(id);

            return NoContent();
        }
    }
}
