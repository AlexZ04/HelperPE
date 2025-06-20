using HelperPE.Common.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelperPE.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserModel loginModel)
        {
            return Ok();
        }
    }
}
