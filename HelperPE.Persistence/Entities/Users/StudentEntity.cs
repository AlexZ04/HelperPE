using HelperPE.Common.Enums;
using HelperPE.Persistence.Entities.Events;
using HelperPE.Persistence.Entities.Faculty;

namespace HelperPE.Persistence.Entities.Users
{
    public class StudentEntity : UserEntity
    {
        public StudentEntity() : base(UserRole.Student) {}
        public StudentEntity(UserRole userRole) : base(userRole) {}

        public required int Course { get; set; }
        public required string Group { get; set; }
        public required FacultyEntity Faculty { get; set; }

        // посещения
        public List<EventAttendanceEntity> EventsAttendances { get; set; } = new List<EventAttendanceEntity>();
    }
}
