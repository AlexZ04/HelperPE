using HelperPE.Application.Services;
using HelperPE.Common.Constants;
using HelperPE.Infrastructure.Filters;
using HelperPE.Infrastructure.Utilities;
using HelperPE.Persistence.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("events")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetEvents()
        {
            return Ok();
        }

        [HttpGet("events/{eventId}")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetEventInfo([FromRoute] Guid eventId)
        {
            return Ok(await _sportsService.GetEventInfo(eventId));
        }

        [HttpGet("profile/{studentId}")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetUserInfo([FromRoute] Guid studentId)
        {
            return Ok(await _curatorService.GetUserInfo(studentId));
        }

        [HttpPut("event/check/{eventId}")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> ApproveStudentApplication(
            [FromRoute] Guid eventId, [FromQuery] Guid studentId)
        {
            await _curatorService.EditEventApplicationStatus(eventId, studentId);

            return Ok();
        }

        [HttpDelete("event/check/{eventId}")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> DeclineStudentApplication(
            [FromRoute] Guid eventId, [FromQuery] Guid studentId)
        {
            await _curatorService.EditEventApplicationStatus(eventId, studentId, false);

            return Ok();
        }

        [HttpGet("group")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetListOfGroup(string groupNumber)
        {
            return Ok();
        }

        [HttpGet("faculties")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetCuratorFaculties()
        {
            return Ok(await _curatorService.GetCuratorFaculties(UserDescriptor.GetUserId(User)));
        }

        [HttpGet("event/applications")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetListOfEventsApplications()
        {
            return Ok();
        }
    }
}
