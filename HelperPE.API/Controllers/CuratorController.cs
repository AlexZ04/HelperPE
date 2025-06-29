using HelperPE.Application.Services;
using HelperPE.Common.Constants;
using HelperPE.Common.Models.Curator;
using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Profile;
using HelperPE.Infrastructure.Filters;
using HelperPE.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelperPE.API.Controllers
{
    [Route("api/curator")]
    [ApiController]
    public class CuratorController : ControllerBase
    {
        private readonly ICuratorService _curatorService;
        private readonly ISportsService _sportsService;

        public CuratorController(
            ICuratorService curatorService,
            ISportsService sportsService)
        {
            _curatorService = curatorService;
            _sportsService = sportsService;
        }

        /// <summary>
        /// Get a list of events of your faculties for the last month
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(EventListModel), StatusCodes.Status200OK)]
        [HttpGet("events")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await _curatorService.GetListOfEvents(UserDescriptor.GetUserId(User)));
        }

        /// <summary>
        /// Get definite event info
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Event</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(EventFullModel), StatusCodes.Status200OK)]
        [HttpGet("events/{eventId}")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetEventInfo([FromRoute] Guid eventId)
        {
            return Ok(await _sportsService.GetEventInfo(eventId));
        }

        /// <summary>
        /// Get student info with his activities
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(UserActivitiesModel), StatusCodes.Status200OK)]
        [HttpGet("profile/{studentId}")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetUserInfo([FromRoute] Guid studentId)
        {
            return Ok(await _curatorService.GetUserInfo(studentId));
        }

        /// <summary>
        /// Approve student application for event participance
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">User or event not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("event/check/{eventId}")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> ApproveStudentApplication(
            [FromRoute] Guid eventId, [FromQuery] Guid studentId)
        {
            await _curatorService.EditEventApplicationStatus(eventId, studentId);

            return Ok();
        }

        /// <summary>
        /// Reject student application for event participance
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">User or event not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("event/check/{eventId}")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> DeclineStudentApplication(
            [FromRoute] Guid eventId, [FromQuery] Guid studentId)
        {
            await _curatorService.EditEventApplicationStatus(eventId, studentId, false);

            return Ok();
        }

        /// <summary>
        /// Get list of students of group
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(StudentsGroupModal), StatusCodes.Status200OK)]
        [HttpGet("group")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetListOfGroup(string groupNumber)
        {
            return Ok(await _curatorService.GetStudentsGroup(groupNumber));
        }

        /// <summary>
        /// Get list of faculties that the person curates
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(FacultiesModal), StatusCodes.Status200OK)]
        [HttpGet("faculties")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetCuratorFaculties()
        {
            return Ok(await _curatorService
                .GetCuratorFaculties(UserDescriptor.GetUserId(User)));
        }

        /// <summary>
        /// Get list of actual events applications
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ApplicationsListModel), StatusCodes.Status200OK)]
        [HttpGet("event/applications")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetListOfEventsApplications()
        {
            return Ok(await _curatorService
                .GetListOfEventsApplications(UserDescriptor.GetUserId(User)));
        }

        /// <summary>
        /// Create other activity for student
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Validation errors</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("activity/student")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> CreateOtherActivity(
            [FromBody] OtherActivityCreateModel activity,
            [FromQuery] Guid studentId)
        {
            await _curatorService.CreateOtherActivity(activity,
                studentId, UserDescriptor.GetUserId(User));

            return Ok();
        }
    }
}
