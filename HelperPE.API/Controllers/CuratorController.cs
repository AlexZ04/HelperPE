using HelperPE.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelperPE.API.Controllers
{
    [Route("api/curator")]
    [ApiController]
    public class CuratorController : ControllerBase
    {
        private readonly ICuratorService _curatorService;

        public CuratorController(ICuratorService curatorService)
        {
            _curatorService = curatorService;
        }
    }
}
