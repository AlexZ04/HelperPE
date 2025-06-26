using HelperPE.Common.Models.Attendances;

namespace HelperPE.Common.Models.Event
{
    public class EventFullModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int ClassesAmount { get; set; } = 1;
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public required FacultyDTO Faculty { get; set; }
        public List<EventAttendanceModel> Attendances { get; set; }
            = new List<EventAttendanceModel>();
    }
}
