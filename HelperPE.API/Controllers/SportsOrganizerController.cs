using HelperPE.Application.Services;
using HelperPE.Common.Constants;
using HelperPE.Common.Enums;
using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Profile;
using HelperPE.Infrastructure.Filters;
using HelperPE.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelperPE.API.Controllers
{
    [Route("api/sports")]
    [ApiController]
    public class SportsOrganizerController : ControllerBase
    {
        private readonly ISportsService _sportsService;

        public SportsOrganizerController(ISportsService sportsService)
        {
            _sportsService = sportsService;
        }

        /// <summary>
        /// Get list of events created by sports organizer
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(EventListModel), StatusCodes.Status200OK)]
        [HttpGet("events")]
        [Authorize(Roles = RolesCombinations.SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetCreatedEvents()
        {
            return Ok(await _sportsService.GetSportsOrgEventList(UserDescriptor.GetUserId(User)));
        }

        /// <summary>
        /// Get definite event info
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Event not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(EventFullModel), StatusCodes.Status200OK)]
        [HttpGet("events/{id}")]
        [Authorize(Roles = RolesCombinations.SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetEventInfo([FromRoute] Guid id)
        {
            return Ok(await _sportsService.GetEventInfo(id));
        }

        /// <summary>
        /// Create info on sports organizer faculty
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("events")]
        [Authorize(Roles = RolesCombinations.SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateModel eventModel)
        {
            await _sportsService.CreateEvent(UserDescriptor.GetUserId(User), eventModel);

            return Ok();
        }

        /// <summary>
        /// Delete event
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Event not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("events/{id}")]
        [Authorize(Roles = RolesCombinations.SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
        {
            await _sportsService.DeleteEvent(id);

            return Ok();
        }

        /// <summary>
        /// Edit application status
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Event or student not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("events/{id}/application")]
        [Authorize(Roles = RolesCombinations.SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> EditEventApplicationStatus([FromRoute] Guid id,
            [FromQuery] Guid userId,
            [FromQuery] SportsOrgEventStatus status)
        {
            await _sportsService.EditEventApplicationStatus(id, userId, status);

            return Ok();
        }
    }
}
