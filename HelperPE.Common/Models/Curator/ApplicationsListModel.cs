using HelperPE.Common.Models.Attendances;

namespace HelperPE.Common.Models.Curator
{
    public class ApplicationsListModel
    {
        public List<EventAttendanceFullModel> Attendances { get; set; } 
            = new List<EventAttendanceFullModel>();
    }
}
