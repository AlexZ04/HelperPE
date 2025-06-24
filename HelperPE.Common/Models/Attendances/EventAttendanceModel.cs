using HelperPE.Common.Enums;
using HelperPE.Common.Models.Event;

namespace HelperPE.Common.Models.Attendances
{
    public class EventAttendanceModel
    {
        public required EventModel Event { get; set; }
        public EventApplicationStatus Status { get; set; } = EventApplicationStatus.Pending;
    }
}
