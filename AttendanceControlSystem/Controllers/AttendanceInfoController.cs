using AttendanceControlSystem.Entity;
using AttendanceControlSystem.Interfaces;
using AttendanceControlSystem.Models.AttendanceInfoModels;
using AttendanceControlSystem.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceControlSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleConstants.Admin)]
    public class AttendanceInfoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceInfoService _attendanceInfoService;
        public AttendanceInfoController(IAttendanceInfoService attendanceInfoService, IMapper mapper)
        {
            _attendanceInfoService = attendanceInfoService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllAttendanceInfos")]
        public async Task<List<AttendanceInfo>> GetAll() =>
            await _attendanceInfoService.GetAllAttendanceInfoAsync();

        [HttpGet]
        [Route("GetAttendanceInfoById/Id")]
        public async Task<IActionResult> Get(string id)
        {
            var attendanceInfo = await _attendanceInfoService.GetByIdAsync(id);
            if (attendanceInfo == null)
            {
                throw new Exception($"There is no attendanceInfo with such id '{id}'");
            }

            return Ok(attendanceInfo);
        }

        [HttpPost]
        [Route("CreateAttendanceInfo")]
        public async Task<IActionResult> Create([FromForm] CreateAttendanceInfoModel createAttendanceInfoModel)
        {
            var attendanceInfo = _mapper.Map<AttendanceInfo>(createAttendanceInfoModel);

            await _attendanceInfoService.CreateAsync(attendanceInfo);
            return CreatedAtAction(nameof(GetAll), new { Id = attendanceInfo.Id }, attendanceInfo);
        }

        [HttpPost]
        [Route("UpdateAttendanceInfo")]
        public async Task<IActionResult> Update([FromBody] AttendanceInfoModel attendanceInfoModel)
        {
            var attendanceInfo = _mapper.Map<AttendanceInfo>(attendanceInfoModel);
            await _attendanceInfoService.UpdateAsync(attendanceInfoModel.Id, attendanceInfo);
            return Ok($"The attendanceInfo with id '{attendanceInfoModel.Id}' was successfully updated");
        }

        [HttpDelete]
        [Route("DeleteAttendanceInfo/id")]
        public async Task<IActionResult> Delete(string id)
        {
            await _attendanceInfoService.RemoveAsync(id);
            return NoContent();
        }
    }
}
