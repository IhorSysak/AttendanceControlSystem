using AttendanceControlSystem.Entity;
using AttendanceControlSystem.Interfaces;
using AttendanceControlSystem.Models.GroupModel;
using AttendanceControlSystem.Models.StudentModels;
using AttendanceControlSystem.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Xml.Linq;

namespace AttendanceControlSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleConstants.Admin)]
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
        public async Task<List<Student>> GetAllAsync() =>
            await _studentService.GetAllStudentAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] string id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                throw new Exception($"There is no student with such id '{id}'");

            return Ok(student);
        }

        [HttpGet("GetStundetByParameters")]
        public async Task<IActionResult> FindStudentByParametesAsync([FromQuery] SearchStudentModel searchStudentModel)
        {
            var student = await _studentService.GetStudentByParametetsAsync(i => i.FullName == searchStudentModel.FullName && i.Group == searchStudentModel.Group && i.Course == searchStudentModel.Course);
            if (student == null)
                throw new Exception("There is no student with such parameters");

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateStudentModel createStudentModel)
        {
            var student = _mapper.Map<Student>(createStudentModel);

            await _studentService.CreateAsync(student);
            return CreatedAtAction(nameof(GetAllAsync), new { Id = student.Id }, student);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] StudentModel studentModel)
        {
            var student = _mapper.Map<Student>(studentModel);
            await _studentService.UpdateAsync(studentModel.Id, student);
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            await _studentService.RemoveAsync(id);
            return NoContent();
        }
    }
}
