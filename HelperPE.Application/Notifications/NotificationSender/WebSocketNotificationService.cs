using HelperPE.Common.Models.Notificatios;
using HelperPE.Common.Models.Pairs;

namespace HelperPE.Application.Notifications.NotificationSender
{
    public class WebSocketNotificationService : IWebSocketNotificationService
    {
        private readonly WebSocketService _webSocketService;

        public WebSocketNotificationService(WebSocketService webSocketService)
        {
            _webSocketService = webSocketService;
        }
        
        public void NotifyPairAttendanceSubmitted(PairAttendanceProfileModel attendance)
        {
            var notification = new WebSocketNotificationModel
            {
                Message = "New pair attendance",
                Data = attendance
            };

            _webSocketService.BroadcastMessage(notification);
        }

        public void NotifyPairAttendanceDeleted(PairAttendanceProfileModel attendance)
        {
            var notification = new WebSocketNotificationModel
            {
                Message = "Pair attendance deleted",
                Data = attendance
            };

            _webSocketService.BroadcastMessage(notification);
        }

    }
}
