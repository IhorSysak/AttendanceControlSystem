using AttendanceControlSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceControlSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleConstants.Teacher)]
    public class SearchingFilterController : ControllerBase
    {
        public SearchingFilterController() 
        {

        }
    }
}
