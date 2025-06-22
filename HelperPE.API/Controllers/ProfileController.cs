using HelperPE.Common.Constants;
using HelperPE.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelperPE.API.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet("student")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetStudentProfile()
        {
            return Ok();
        }

        [HttpGet("teacher")]
        public async Task<IActionResult> GetTeacherProfile()
        {
            return Ok();
        }

        [HttpGet("curator")]
        public async Task<IActionResult> GetCuratorProfile()
        {
            return Ok();
        } 
    }
}
