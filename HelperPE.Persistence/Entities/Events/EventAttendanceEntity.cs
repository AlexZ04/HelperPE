using HelperPE.Common.Enums;
using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Entities.Events
{
    public class EventAttendanceEntity
    {
        public Guid EventId { get; set; }
        public Guid StudentId { get; set; }

        public required EventEntity Event { get; set; }
        public required StudentEntity Student { get; set; }
        public EventApplicationStatus Status { get; set; } = EventApplicationStatus.Pending;
    }
}
