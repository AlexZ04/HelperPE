using HelperPE.Common.Enums;
using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Entities.Pairs
{
    public class PairAttendanceEntity
    {
        public Guid StudentId { get; set; }
        public Guid PairId { get; set; }
        public required StudentEntity Student { get; set; }
        public required PairEntity Pair { get; set; }
        public PairAttendanceStatus Status { get; set; } = PairAttendanceStatus.Pending;
        public int ClassesAmount { get; set; } = 1;
    }
}
