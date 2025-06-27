using HelperPE.Common.Enums;
using HelperPE.Common.Models.Curator;

namespace HelperPE.Common.Models.Pairs
{
    public class PairAttendanceProfileModel
    {
        public required PairShortModel Pair { get; set; }
        public int ClassesAmount { get; set; } = 1;
        public PairAttendanceStatus Status { get; set; }
        public required StudentProfileShortModel Student { get; set; }
    }
}
