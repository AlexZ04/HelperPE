using HelperPE.Common.Models.Pairs;

namespace HelperPE.Common.Models.Teacher
{
    public class PairAttendancesListModel
    {
        public List<PairAttendanceProfileModel> Attendances { get; set; } 
            = new List<PairAttendanceProfileModel>();
    }
}
