using HelperPE.Application.Services;
using HelperPE.Common.Models.Auth;
using HelperPE.Infrastructure.Filters;
using HelperPE.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelperPE.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserModel loginModel)
        {
            return Ok(await _authService.Login(loginModel));
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel refreshToken)
        {
            return Ok(await _authService.RefreshToken(refreshToken));
        }

        [HttpPost("logout")]
        [Authorize]
        [CheckTokens]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout(HttpContext.GetTokenAsync("access_token").Result,
                UserDescriptor.GetUserId(User));

            return Ok();
        }
    }
}
