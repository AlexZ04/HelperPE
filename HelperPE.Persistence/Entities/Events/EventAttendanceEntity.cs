using HelperPE.Common.Enums;
using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Entities.Events
{
    public class EventAttendanceEntity
    {
        public required EventEntity Event { get; set; }
        public required StudentEntity Student { get; set; }
        public EventApplicationStatus Status { get; set; } = EventApplicationStatus.Pending;
    }
}
