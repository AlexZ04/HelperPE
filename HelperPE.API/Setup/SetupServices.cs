using HelperPE.Application.Services;
using HelperPE.Application.Services.Implementations;

namespace HelperPE.API.Setup
{
    public class SetupServices
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenServiceImpl>();

            services.AddTransient<IProfileService, ProfileServiceImpl>();
            services.AddTransient<IAdminService, AdminServiceImpl>();
        }
    }
}
