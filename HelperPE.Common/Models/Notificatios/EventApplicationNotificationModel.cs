namespace HelperPE.Common.Models.Notificatios
{
    public class EventApplicationNotificationModel
    {
        public Guid EventId { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
    }
}
