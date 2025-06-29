using HelperPE.Application.Services;
using HelperPE.Common.Constants;
using HelperPE.Infrastructure.Filters;
using HelperPE.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HelperPE.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("teachers")]
        [Authorize(Roles = RolesCombinations.ADMIN)]
        [CheckTokens]
        public async Task<IActionResult> GetTeachers()
        {
            return Ok(await _adminService.GetTeachers());
        }

        [HttpGet("curators")]
        [Authorize(Roles = RolesCombinations.ADMIN)]
        [CheckTokens]
        public async Task<IActionResult> GetCurators()
        {
            return Ok(await _adminService.GetCurators());
        }
        [HttpGet("faculties")]
        [Authorize]
        [CheckTokens]
        public async Task<IActionResult> GetFaculties()
        {
            return Ok(await _adminService.GetFaculties());
        }

        [HttpPost("curator")]
        [Authorize(Roles = RolesCombinations.ADMIN)]
        [CheckTokens]
        public async Task<IActionResult> AddCurator([Required] Guid userId, [Required] Guid facultyId)
        {
            await _adminService.AddСurator(userId, facultyId);

            return Ok();
        }

        [HttpDelete("curator")]
        [Authorize(Roles = RolesCombinations.ADMIN)]
        [CheckTokens]
        public async Task<IActionResult> DeleteCurator([Required] Guid userId, [Required] Guid facultyId)
        {
            await _adminService.DeleteСurator(userId, facultyId);
            return Ok();
        }
    }
}
