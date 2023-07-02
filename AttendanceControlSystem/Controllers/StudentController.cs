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
        public async Task<List<Student>> GetAll() =>
            await _studentService.GetAllStudentAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
            {
                throw new Exception($"There is no student with such id '{id}'");
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentModel createStudentModel) 
        {
            var student = _mapper.Map<Student>(createStudentModel);

            await _studentService.CreateAsync(student);
            return CreatedAtAction(nameof(GetAll), new { Id = student.Id }, student);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StudentModel studentModel)
        {
            var student = _mapper.Map<Student>(studentModel);
            await _studentService.UpdateAsync(studentModel.Id, student);
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _studentService.RemoveAsync(id);
            return NoContent();
        }
    }
}
