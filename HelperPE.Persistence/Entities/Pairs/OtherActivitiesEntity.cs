using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Entities.Pairs
{
    public class OtherActivitiesEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Comment { get; set; }
        public int ClassesAmount { get; set; } = 1;
        public required StudentEntity Student { get; set; }
        public required TeacherEntity Teacher { get; set; }
    }
}
