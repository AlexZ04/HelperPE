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

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
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
        public async Task<IActionResult> CreatePair([FromQuery] Guid subjectId)
        {
            await _teacherService.CreatePair(subjectId, UserDescriptor.GetUserId(User));

            return Ok();
        }

        [HttpPut("pairs/{pairId}")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> ApproveStudentOnPair(
            [FromRoute] Guid pairId, [FromQuery] Guid userId, [FromQuery] int classesAmount = 1)
        {
            await _teacherService.EditPairAttendanceStatus(pairId, userId, classesAmount);

            return Ok();
        }

        [HttpDelete("pairs/{pairId}")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> DeclineStudentOnPair(
            [FromRoute] Guid pairId, [FromQuery] Guid userId, [FromQuery] int classesAmount = 1)
        {
            await _teacherService.EditPairAttendanceStatus(pairId, userId, classesAmount, false);

            return Ok();
        }

        [HttpGet("subjects")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetTeacherSubjects()
        {
            return Ok(await _teacherService.GetTeacherSubjects(UserDescriptor.GetUserId(User)));
        }

        [HttpGet("attendances")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetActualAttendancesRequests()
        {
            return Ok(await _teacherService.GetPairAttendances
                (UserDescriptor.GetUserId(User)));
        }

        [HttpGet("attendances/{pairId}")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetPairAttendances([FromRoute] Guid pairId)
        {
            return Ok(await _teacherService.GetPairAttendances(pairId,
                UserDescriptor.GetUserId(User))); 
        }

        [HttpGet("attendances-live")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetActualAttendancesLive()
        {   
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {

            }
            else
            {

            }
            return Ok();
        }
    }
}
