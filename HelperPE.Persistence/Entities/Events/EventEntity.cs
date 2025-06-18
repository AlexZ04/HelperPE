namespace HelperPE.Persistence.Entities.Events
{
    public class EventEntity
    {
        public Guid EventId { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public int ClassesAmount { get; set; } = 1;
        public string? Description { get; set; } 
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public List<EventAttendanceEntity> Attendances { get; set; } = new List<EventAttendanceEntity>();
    }
}
