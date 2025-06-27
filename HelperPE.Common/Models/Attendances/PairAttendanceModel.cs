using HelperPE.Common.Enums;
using HelperPE.Common.Models.Pairs;

namespace HelperPE.Common.Models.Attendances
{
    public class PairAttendanceModel
    {
        public int ClassesAmount { get; set; } = 1; 
        public required PairModel Pair { get; set; }
        public PairAttendanceStatus Status { get; set; }
    }
}
