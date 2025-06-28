using HelperPE.Common.Models.Pairs;

namespace HelperPE.Application.Notifications.NotificationSender
{
    public interface IWebSocketNotificationService
    {
        void NotifyPairAttendanceSubmitted(PairAttendanceShortModel attendance);
    }
}
