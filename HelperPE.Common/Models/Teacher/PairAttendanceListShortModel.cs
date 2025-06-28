using HelperPE.Common.Models.Pairs;

namespace HelperPE.Common.Models.Teacher
{
    public class PairAttendanceListShortModel
    {
        public List<PairAttendanceShortModel> Attendances { get; set; } 
            = new List<PairAttendanceShortModel>();
    }
}
