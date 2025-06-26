using HelperPE.Common.Models.Profile;

namespace HelperPE.Common.Models.Curator
{
    public class StudentProfileActivitiesModal
    {
        public required StudentProfileDTO Student { get; set; }
        public required UserActivitiesModel StudentActivities { get; set; }
    }
}
