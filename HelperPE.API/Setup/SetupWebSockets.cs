using HelperPE.Application.Notifications;
using HelperPE.Common.ProjectSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HelperPE.API.Setup
{
    public class SetupWebSockets
    {
        public static void AddWebSockets(WebApplication app)
        {
            var webSocketService = app.Services.GetRequiredService<WebSocketService>();
            webSocketService.Start();
        }

        public static void UseWebSockets(WebApplication app)
        {
            app.UseWebSockets();
        }
    }
}
