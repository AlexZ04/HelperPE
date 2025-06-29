using HelperPE.Application.Services;
using HelperPE.Common.Constants;
using HelperPE.Common.Models;
using HelperPE.Common.Models.Auth;
using HelperPE.Infrastructure.Filters;
using HelperPE.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelperPE.API.Controllers
{
    [Route("api/avatar")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly IAvatarService _avatarService;

        public AvatarController(IAvatarService avatarService)
        {
            _avatarService = avatarService;
        }

        /// <summary>
        /// Get user avatar by id
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
        [HttpGet("")]
        [Authorize]
        [CheckTokens]
        public async Task<IActionResult> GetFile(Guid id)
        {
            var (content, contentType, fileName) = await _avatarService.GetFile(id);
            return File(content, contentType);
            //var (content, contentType, fileName) = await _documentsService.Ge1tFile(id);
            //return File(content, contentType, fileName); -- ссылка на скачивания файла
            //return File(content, contentType); -- файл
        }

        /// <summary>
        /// Upload avatar
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(GuidResponseModel), StatusCodes.Status200OK)]
        [HttpPost("")]
        [Authorize]
        [CheckTokens]
        public async Task<IActionResult> AddAvatar(IFormFile avatar)
        {
            return Ok(await _avatarService.AddAvatar(UserDescriptor.GetUserId(User), avatar));
        }

        /// <summary>
        /// Delete file by id
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("")]
        [Authorize]
        [CheckTokens]
        public async Task<IActionResult> DeleteAvatar()
        {
            await _avatarService.DeleteAvatar(UserDescriptor.GetUserId(User));
            return Ok();
        }

        /// <summary>
        /// Change user avatar
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(GuidResponseModel), StatusCodes.Status200OK)]
        [HttpPut("")]
        [Authorize]
        [CheckTokens]
        public async Task<IActionResult> ChangeAvatar(IFormFile avatar)
        {
            return Ok(await _avatarService.ChangeAvatar(UserDescriptor.GetUserId(User), avatar));
        }
    }
}
