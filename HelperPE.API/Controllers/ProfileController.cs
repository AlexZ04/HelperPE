using HelperPE.Application.Services;
using HelperPE.Common.Constants;
using HelperPE.Common.Models.Curator;
using HelperPE.Common.Models.Profile;
using HelperPE.Infrastructure.Filters;
using HelperPE.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelperPE.API.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// Get student profile
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(StudentProfileDTO), StatusCodes.Status200OK)]
        [HttpGet("student")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetStudentProfile()
        {
            var userId = UserDescriptor.GetUserId(User);
            if (UserDescriptor.GetUserRole(User) == RolesCombinations.SPORTS)
                return Ok(await _profileService.GetSportsProfileById(userId));

            return Ok(await _profileService.GetStudentProfileById(userId));
        }

        /// <summary>
        /// Get teacher profile
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(TeacherProfileDTO), StatusCodes.Status200OK)]
        [HttpGet("teacher")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetTeacherProfile()
        {
            return Ok(await _profileService.GetTeacherProfileById(UserDescriptor.GetUserId(User)));
        }

        /// <summary>
        /// Get curator profile
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(CuratorProfileDTO), StatusCodes.Status200OK)]
        [HttpGet("curator")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetCuratorProfile()
        {
            return Ok(await _profileService.GetCuratorProfileById(UserDescriptor.GetUserId(User)));
        }

        /// <summary>
        /// Get student activities
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(UserActivitiesModel), StatusCodes.Status200OK)]
        [HttpGet("all-attendances")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetStudenActivities()
        {
            return Ok(await _profileService.GetUserActivities(UserDescriptor.GetUserId(User)));
        }
    }
}
