using HelperPE.Common.Enums;
using HelperPE.Persistence.Entities.Faculty;

namespace HelperPE.Persistence.Entities.Users
{
    public class CuratorEntity : TeacherEntity
    {
        public CuratorEntity() : base(UserRole.Curator) { }

        public List<FacultyEntity> Faculties { get; set; } = new List<FacultyEntity>();
    }
}
