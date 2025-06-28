using HelperPE.Application.Services;
using HelperPE.Common.Constants;
using HelperPE.Infrastructure.Filters;
using HelperPE.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HelperPE.Application.Notifications.NotificationSender;

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

        [HttpGet("events")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetAvailableEvents()
        {
            return Ok(await _studentService.GetAvailableEvents(UserDescriptor.GetUserId(User)));
        }

        [HttpPost("application/{eventId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> SubmitApplicationToEvent([FromRoute] Guid eventId)
        {
            var userId = UserDescriptor.GetUserId(User);
            var userRole = UserDescriptor.GetUserRole(User);

            await _studentService.SubmitApplicationToEvent(eventId, userId, userRole);

            return Ok();
        }

        [HttpGet("application/{eventId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> CheckApplicationStatus([FromRoute] Guid eventId)
        {
            return Ok(await _studentService.CheckApplicationStatus(eventId, UserDescriptor.GetUserId(User)));
        }

        [HttpDelete("application/{eventId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> RestrictApplication([FromRoute] Guid eventId)
        {
            await _studentService.RestrictApplication(eventId, UserDescriptor.GetUserId(User));

            return Ok();
        }

        [HttpGet("pairs")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetAvailablePairs()
        {
            return Ok(await _studentService.GetAvailablePairs(UserDescriptor.GetUserId(User)));
        }

        [HttpPost("attendance/{pairId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> SubmitAttendanceToPair([FromRoute] Guid pairId)
        {
            var userId = UserDescriptor.GetUserId(User);

            await _studentService.SubmitAttendanceToPair(pairId, userId);

            return Ok();
        }

        [HttpGet("attendance/{pairId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> CheckAttendanceStatus([FromRoute] Guid pairId)
        {
            return Ok(await _studentService
                .CheckPairAttendanceStatus(pairId, UserDescriptor.GetUserId(User)));
        }

        [HttpDelete("attendance/{pairId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> RestrictAttendance([FromRoute] Guid pairId)
        {
            await _studentService.RestrictPairAttendance(pairId, UserDescriptor.GetUserId(User));

            return Ok();
        }
    }
}
