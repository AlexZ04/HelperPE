﻿using HelperPE.Application.Services;
using HelperPE.Application.Services.Implementations;
using HelperPE.Application.Notifications;
using HelperPE.Application.Notifications.NotificationSender;

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
            services.AddTransient<ISportsService, SportsServiceImpl>();
            services.AddTransient<IStudentService, StudentServiceImpl>();
            services.AddTransient<ICuratorService, CuratorServiceImpl>();
            services.AddTransient<ITeacherService, TeacherServiceImpl>();
            services.AddTransient<IAvatarService, AvatarServiceImpl>();

            services.AddSingleton<WebSocketService>();
            services.AddTransient<IWebSocketNotificationService, WebSocketNotificationService>();
        }
    }
}
