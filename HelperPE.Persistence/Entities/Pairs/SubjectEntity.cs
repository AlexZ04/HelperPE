using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Entities.Pairs
{
    public class SubjectEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public List<TeacherEntity> Teachers { get; set; } = new List<TeacherEntity>();
    }
}
