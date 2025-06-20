using HelperPE.Common.Models.Auth;

namespace HelperPE.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public async Task<TokenResponseModel> TokenResponse()
        {
            throw new NotImplementedException();
        }

        public async Task<TokenResponseModel> RefreshToken()
        {
            throw new NotImplementedException();
        }

        public async Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
