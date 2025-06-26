using HelperPE.Application.Services;
using HelperPE.Common.Constants;
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

        [HttpGet("teacher")]
        [Authorize(Roles = RolesCombinations.TEACHER_AND_CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetTeacherProfile()
        {
            return Ok(await _profileService.GetTeacherProfileById(UserDescriptor.GetUserId(User)));
        }

        [HttpGet("curator")]
        [Authorize(Roles = RolesCombinations.CURATOR)]
        [CheckTokens]
        public async Task<IActionResult> GetCuratorProfile()
        {
            return Ok(await _profileService.GetCuratorProfileById(UserDescriptor.GetUserId(User)));
        }

        [HttpGet("all-attendances")]
        [Authorize(Roles = RolesCombinations.STUDENT_AND_SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetStudenActivities()
        {
            return Ok(await _profileService.GetUserActivities(UserDescriptor.GetUserId(User)));
        }

        [HttpGet("created-events")]
        [Authorize(Roles = RolesCombinations.SPORTS)]
        [CheckTokens]
        public async Task<IActionResult> GetCreatedEvents()
        {
            return Ok(await _profileService.GetSportsOrgEventList(UserDescriptor.GetUserId(User)));
        }
    }
}
