using HelperPE.Application.Services;
using HelperPE.Common.Constants;
using HelperPE.Common.Models.Pairs;
using HelperPE.Common.Models.Teacher;
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

        /// <summary>
        /// Get list of teacher pairs
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(TeacherPairsModel), StatusCodes.Status200OK)]
        [HttpGet("pairs")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetTodayPairs()
        {
            return Ok(await _teacherService.GetTodayPairs(UserDescriptor.GetUserId(User)));
        }

        /// <summary>
        /// Create new pair
        /// </summary>
        /// <response code="200">Success</response>
        /// <responde code="400">Validation errors</responde>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Subject not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("pairs")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> CreatePair([FromQuery] Guid subjectId)
        {
            await _teacherService.CreatePair(subjectId, UserDescriptor.GetUserId(User));

            return Ok();
        }

        /// <summary>
        /// Approve student pair application with classes amount
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Pair or user not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("pairs/{pairId}")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> ApproveStudentOnPair(
            [FromRoute] Guid pairId, [FromQuery] Guid userId, [FromQuery] int classesAmount = 1)
        {
            await _teacherService.EditPairAttendanceStatus(pairId, userId, classesAmount);

            return Ok();
        }

        /// <summary>
        /// Reject student pair application
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Pair or user not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("pairs/{pairId}")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> DeclineStudentOnPair(
            [FromRoute] Guid pairId, [FromQuery] Guid userId, [FromQuery] int classesAmount = 1)
        {
            await _teacherService.EditPairAttendanceStatus(pairId, userId, classesAmount, false);

            return Ok();
        }

        /// <summary>
        /// Get list of teacher subjects
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(SubjectListModel), StatusCodes.Status200OK)]
        [HttpGet("subjects")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetTeacherSubjects()
        {
            return Ok(await _teacherService.GetTeacherSubjects(UserDescriptor.GetUserId(User)));
        }

        /// <summary>
        /// Get list of actual attendances for all pairs
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(PairAttendancesListModel), StatusCodes.Status200OK)]
        [HttpGet("attendances")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetActualAttendancesRequests()
        {
            return Ok(await _teacherService.GetPairAttendances
                (UserDescriptor.GetUserId(User)));
        }

        /// <summary>
        /// Get list of pending pair attendances for definite pair
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Pair not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(PairAttendanceListShortModel), StatusCodes.Status200OK)]
        [HttpGet("attendances/pending/{pairId}")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetPendingPairAttendances([FromRoute] Guid pairId)
        {
            return Ok(await _teacherService.GetPendingPairAttendances(pairId,
                UserDescriptor.GetUserId(User))); 
        }

        /// <summary>
        /// Get list of solved pair attendances for definite pair
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Pair not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(PairAttendanceListShortModel), StatusCodes.Status200OK)]
        [HttpGet("attendances/solved/{pairId}")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetSolvedPairAttendances([FromRoute] Guid pairId)
        {
            return Ok(await _teacherService.GetSolvedPairAttendances(pairId,
                UserDescriptor.GetUserId(User)));
        }

        /// <summary>
        /// Get list of all pair attendances for definite pair
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Pair not found</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(PairAttendanceListShortModel), StatusCodes.Status200OK)]
        [HttpGet("attendances/all/{pairId}")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetAllPairAttendances([FromRoute] Guid pairId)
        {
            return Ok(await _teacherService.GetAllPairAttendances(pairId,
                UserDescriptor.GetUserId(User)));
        }
    }
}
