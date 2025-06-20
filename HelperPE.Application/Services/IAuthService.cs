using HelperPE.Common.Models.Auth;

namespace HelperPE.Application.Services
{
    public interface IAuthService
    {
        public Task<TokenResponseModel> TokenResponse();
        public Task<TokenResponseModel> RefreshToken();
        public Task Logout();
    }
}
