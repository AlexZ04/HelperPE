using HelperPE.Application.Services;
using HelperPE.Common.Constants;
using HelperPE.Infrastructure.Filters;
using HelperPE.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelperPE.API.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly ITimeService _timeService;

        public TeacherController(
            ITeacherService teacherService, ITimeService timeService)
        {
            _teacherService = teacherService;
            _timeService = timeService;
        }

        [HttpGet("pairs")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetTodayPairs()
        {
            return Ok(await _teacherService.GetTodayPairs(UserDescriptor.GetUserId(User)));
        }

        [HttpPost("pairs")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> CreatePair()
        {
            return Ok(_timeService.GetSemesterNumber());
        }

        [HttpPut("pairs/{pairId}")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> ApproveStudentOnPair([FromRoute] Guid pairId, [FromQuery] Guid userId)
        {
            return Ok();
        }

        [HttpDelete("pairs/{pairId}")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> DeclineStudentOnPair([FromRoute] Guid pairId, [FromQuery] Guid userId)
        {
            return Ok();
        }

        [HttpGet("subjects")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetTeacherSubjects()
        {
            return Ok(await _teacherService.GetTeacherSubjects(UserDescriptor.GetUserId(User)));
        }

        [HttpGet("attendance")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetActualAttendancesRequests()
        {
            return Ok();
        }
    }
}
