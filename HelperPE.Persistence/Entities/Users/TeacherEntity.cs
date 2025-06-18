using HelperPE.Common.Enums;

namespace HelperPE.Persistence.Entities.Users
{
    public class TeacherEntity : UserEntity
    {
        public TeacherEntity() : base(UserRole.Teacher) { }
        public TeacherEntity(UserRole role) : base(role) { }

        // предметы
        // пары
    }
}
