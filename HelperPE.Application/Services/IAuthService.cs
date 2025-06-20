using HelperPE.Common.Models.Auth;

namespace HelperPE.Application.Services
{
    public interface IAuthService
    {
        public Task<TokenResponseModel> Login(LoginUserModel loginModel);
        public Task<TokenResponseModel> RefreshToken(RefreshTokenModel refreshModel);
        public Task Logout();
    }
}
