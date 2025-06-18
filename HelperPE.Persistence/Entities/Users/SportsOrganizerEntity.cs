using HelperPE.Common.Enums;
using HelperPE.Persistence.Entities.Events;

namespace HelperPE.Persistence.Entities.Users
{
    public class SportsOrganizerEntity : StudentEntity
    {
        public SportsOrganizerEntity() : base(UserRole.SportsOrganizer) { }
        public DateTime AppointmentDate { get; set; } = DateTime.UtcNow;
        public List<EventEntity> Events { get; set; } = new List<EventEntity>();
    }
}
