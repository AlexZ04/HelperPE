using HelperPE.Common.Models.Pairs;

namespace HelperPE.Application.Notifications.NotificationSender
{
    public interface IWebSocketNotificationService
    {
        public void NotifyPairAttendanceSubmitted(PairAttendanceProfileModel attendance);
        public void NotifyPairAttendanceDeleted(PairAttendanceProfileModel attendance);
        public void TestMessage(string message);
    }
}
