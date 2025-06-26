using HelperPE.Common.Enums;
using HelperPE.Common.Models.Profile;

namespace HelperPE.Common.Models.Attendances
{
    public class ProfileAttendanceEventModel
    {
        public required StudentProfileDTO Profile { get; set; }
        public EventApplicationStatus Status { get; set; } = EventApplicationStatus.Pending;
    }
}
