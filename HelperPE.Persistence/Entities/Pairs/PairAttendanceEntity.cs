using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Entities.Pairs
{
    public class PairAttendanceEntity
    {
        public required StudentEntity Student { get; set; }
        public required PairEntity Pair { get; set; }
        public int ClassesAmount { get; set; } = 1;
    }
}
