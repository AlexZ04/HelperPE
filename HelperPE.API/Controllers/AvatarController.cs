using HelperPE.Application.Services;
using HelperPE.Common.Constants;
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


        [HttpPost("")]
        [Authorize]
        [CheckTokens]
        public async Task<IActionResult> AddAvatar(IFormFile avatar)
        {
            return Ok(await _avatarService.AddAvatar(UserDescriptor.GetUserId(User), avatar));
        }

        [HttpDelete("")]
        [Authorize]
        [CheckTokens]
        public async Task<IActionResult> DeleteAvatar()
        {
            await _avatarService.DeleteAvatar(UserDescriptor.GetUserId(User));
            return Ok();
        }
       
        [HttpPut("")]
        [Authorize]
        [CheckTokens]
        public async Task<IActionResult> ChangeAvatar(IFormFile avatar)
        {
            return Ok(await _avatarService.ChangeAvatar(UserDescriptor.GetUserId(User), avatar));
        }
    }
}
