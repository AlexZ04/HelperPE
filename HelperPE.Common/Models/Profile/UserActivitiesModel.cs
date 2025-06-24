using HelperPE.Common.Models.Attendances;

namespace HelperPE.Common.Models.Profile
{
    public class UserActivitiesModel
    {
        public List<PairAttendanceModel> Pairs { get; set; } 
            = new List<PairAttendanceModel>();

        public List<OtherActivityModel> OtherActivities { get; set; } 
            = new List<OtherActivityModel>();

        public List<EventAttendanceModel> Events { get; set; } 
            = new List<EventAttendanceModel>();
    }
}
