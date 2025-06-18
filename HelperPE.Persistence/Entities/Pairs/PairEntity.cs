using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Entities.Pairs
{
    public class PairEntity
    {
        public Guid PairId { get; set; } = Guid.NewGuid();
        public int PairNumber { get; set; } = 1;
        public required TeacherEntity Teacher { get; set; }
        public required SubjectEntity Subject { get; set; }
        public List<PairAttendanceEntity> Attendances { get; set; } = new List<PairAttendanceEntity>();
    }
}
