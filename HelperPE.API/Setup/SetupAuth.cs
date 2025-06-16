using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HelperPE.API.Setup
{
    public class SetupAuth
    {
        public static void AddAuth(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    //options.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    ValidateIssuer = true,
                    //    ValidIssuer = "",
                    //    ValidateAudience = true,
                    //    ValidAudience = "",
                    //    ValidateLifetime = true,
                    //    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    //    ValidateIssuerSigningKey = true,
                    //};
                });

            builder.Services.AddAuthorization();
        }

        public static void UseAuth(WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
