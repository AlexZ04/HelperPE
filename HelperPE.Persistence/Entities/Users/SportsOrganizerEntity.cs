using HelperPE.Common.Enums;

namespace HelperPE.Persistence.Entities.Users
{
    public class SportsOrganizerEntity : StudentEntity
    {
        public SportsOrganizerEntity() : base(UserRole.SportsOrganizer) { }
        public DateTime AppointmentDate { get; set; } = DateTime.UtcNow;

        // мероприятия
    }
}
