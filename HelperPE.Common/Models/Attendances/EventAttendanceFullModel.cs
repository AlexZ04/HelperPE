using HelperPE.Common.Enums;
using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Profile;

namespace HelperPE.Common.Models.Attendances
{
    public class EventAttendanceFullModel
    {
        public required StudentProfileDTO Profile { get; set; }
        public required EventModel Event { get; set; }
        public EventApplicationStatus Status { get; set; } = EventApplicationStatus.Pending;
    }
}
