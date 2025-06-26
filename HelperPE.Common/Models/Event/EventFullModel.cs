using HelperPE.Common.Models.Attendances;
using HelperPE.Common.Models.Profile;

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
        public List<ProfileAttendanceEventModel> Attendances { get; set; }
            = new List<ProfileAttendanceEventModel>();
    }
}
