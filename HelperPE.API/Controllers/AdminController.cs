using HelperPE.Application.Services;
using HelperPE.Common.Constants;
using HelperPE.Infrastructure.Filters;
using HelperPE.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        //[Authorize(Roles = RolesCombinations.ADMIN)]
        //[CheckTokens]
        public async Task<IActionResult> GetCurators()
        {
            return Ok(await _adminService.GetCurators());
        }

    }
}
