using HelperPE.Application.Services;
using HelperPE.Common.Constants;
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

        [HttpGet("events")]
        [Authorize(Roles = RolesCombinations.SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetCreatedEvents()
        {
            return Ok(await _sportsService.GetSportsOrgEventList(UserDescriptor.GetUserId(User)));
        }

        [HttpGet("events/{id}")]
        [Authorize(Roles = RolesCombinations.SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetEventInfo([FromRoute] Guid id)
        {
            return Ok();
        }

        [HttpPost("events")]
        [Authorize(Roles = RolesCombinations.SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> CreateEvent()
        {
            return Ok();
        }
    }
}
