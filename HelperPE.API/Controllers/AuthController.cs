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

        /// <summary>
        /// Login with email and password
        /// </summary>
        /// <response code="200">Get access and refresh tokens</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(List<TokenResponseModel>), StatusCodes.Status200OK)]
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserModel loginModel)
        {
            return Ok(await _authService.Login(loginModel));
        }

        /// <summary>
        /// Get new pairs of tokens with refresh token
        /// </summary>
        /// <response code="200">Get access and refresh tokens</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(List<TokenResponseModel>), StatusCodes.Status200OK)]
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel refreshToken)
        {
            return Ok(await _authService.RefreshToken(refreshToken));
        }

        /// <summary>
        /// Get new pairs of tokens with refresh token
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("logout")]
        [Authorize]
        [CheckTokens]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout(HttpContext.GetTokenAsync("access_token").Result,
                UserDescriptor.GetUserId(User));

            return Ok();
        }

        /// <summary>
        /// Get info about current user session (role)
        /// </summary>
        /// <response code="200">Get access and refresh tokens</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(SessionModel), StatusCodes.Status200OK)]
        [HttpGet("session")]
        [Authorize]
        [CheckTokens]
        public IActionResult GetCurrentSession()
        {
            var sessionModel = new SessionModel
            {
                Role = UserDescriptor.GetUserRole(User)
            };

            return Ok(sessionModel);
        }
    }
}
