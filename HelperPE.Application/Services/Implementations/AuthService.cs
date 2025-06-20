using HelperPE.Common.Models.Auth;

namespace HelperPE.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public async Task<TokenResponseModel> Login(LoginUserModel loginModel)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenResponseModel> RefreshToken(RefreshTokenModel refreshModel)
        {
            throw new NotImplementedException();
        }

        public async Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
