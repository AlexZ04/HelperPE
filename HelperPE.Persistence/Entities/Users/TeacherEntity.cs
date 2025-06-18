using HelperPE.Common.Enums;
using HelperPE.Persistence.Entities.Pairs;

namespace HelperPE.Persistence.Entities.Users
{
    public class TeacherEntity : UserEntity
    {
        public TeacherEntity() : base(UserRole.Teacher) { }
        public TeacherEntity(UserRole role) : base(role) { }

        public List<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();
        public List<PairEntity> Pairs { get; set; } = new List<PairEntity>();
    }
}
