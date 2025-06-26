using HelperPE.Application.Services;
using HelperPE.Common.Constants;
using HelperPE.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelperPE.API.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("application/{eventId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> SubmitApplicationToEvent([FromRoute] Guid eventId)
        {
            return Ok();
        }

        [HttpGet("application/{eventId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> CheckApplicationStatus([FromRoute] Guid eventId)
        {
            return Ok();
        }

        [HttpDelete("application/{eventId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> RestrictApplication([FromRoute] Guid eventId)
        {
            return Ok();
        }
    }
}
