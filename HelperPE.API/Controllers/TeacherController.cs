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
        public async Task<IActionResult> GetActualPairs()
        {
            return Ok();
        }

        [HttpGet("pairs/{pairId}")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetPairInfo([FromRoute] Guid pairId)
        {
            return Ok();
        }

        [HttpPost("pairs")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> CreatePair()
        {
            return Ok();
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

        [HttpGet("subjects/teacher")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetTeacherSubjects()
        {
            return Ok(await _teacherService.GetTeacherSubjects(UserDescriptor.GetUserId(User)));
        }

        [HttpGet("pairs/teacher")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetAllTeacherPairs()
        {
            return Ok();
        }
    }
}
