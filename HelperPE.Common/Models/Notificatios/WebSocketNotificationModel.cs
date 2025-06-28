namespace HelperPE.Common.Models.Notificatios
{
    public class WebSocketNotificationModel
    {
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
} 