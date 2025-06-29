using HelperPE.Application.Notifications.NotificationSender;
using HelperPE.Application.Services;
using HelperPE.Common.Constants;
using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Pairs;
using HelperPE.Infrastructure.Filters;
using HelperPE.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelperPE.API.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IWebSocketNotificationService _notificationService;

        public StudentController(IStudentService studentService, IWebSocketNotificationService notificationService)
        {
            _studentService = studentService;
            _notificationService = notificationService;
        }

        [HttpGet("test-socket")]
        public IActionResult Get()
        {
            _notificationService.TestMessage("hello");

            return Ok();
        }

        /// <summary>
        /// Get list of available events 
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(EventListModel), StatusCodes.Status200OK)]
        [HttpGet("events")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetAvailableEvents()
        {
            return Ok(await _studentService.GetAvailableEvents(UserDescriptor.GetUserId(User)));
        }

        /// <summary>
        /// Apply to attend a event
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Action is already done</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Event not found</response>
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

        /// <summary>
        /// Get status of event attendance
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Attendance not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(EventAttendanceStatusModel), StatusCodes.Status200OK)]
        [HttpGet("application/{eventId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> CheckApplicationStatus([FromRoute] Guid eventId)
        {
            try
            {
                return Ok(await _studentService.CheckApplicationStatus(eventId, UserDescriptor.GetUserId(User)));
            }
            catch
            {
                return Ok(new { Status = "DidNotVisit" });
            }
            
        }

        /// <summary>
        /// Delete event attendance application
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Action is already done</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Event not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("application/{eventId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> RestrictApplication([FromRoute] Guid eventId)
        {
            await _studentService.RestrictApplication(eventId, UserDescriptor.GetUserId(User));

            return Ok();
        }

        /// <summary>
        /// Get available pairs
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(PairListModel), StatusCodes.Status200OK)]
        [HttpGet("pairs")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetAvailablePairs()
        {
            return Ok(await _studentService.GetAvailablePairs(UserDescriptor.GetUserId(User)));
        }

        /// <summary>
        /// Apply to attend a event
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Action is already done</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Pair not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("attendance/{pairId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> SubmitAttendanceToPair([FromRoute] Guid pairId)
        {
            var userId = UserDescriptor.GetUserId(User);

            await _studentService.SubmitAttendanceToPair(pairId, userId);

            return Ok();
        }

        /// <summary>
        /// Get status of pair attendance
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Pair not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(PairAttendanceStatusModel), StatusCodes.Status200OK)]
        [HttpGet("attendance/{pairId}")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> CheckAttendanceStatus([FromRoute] Guid pairId)
        {
            try
            {
                return Ok(await _studentService
                    .CheckPairAttendanceStatus(pairId, UserDescriptor.GetUserId(User)));
            }
            catch
            {
                return Ok(new { Status = "DidNotVisit" });
            }
        }

        /// <summary>
        /// Delete pair attendance application
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Action is already done</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Pair not found</response>
        /// <response code="500">Internal server error</response>
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
