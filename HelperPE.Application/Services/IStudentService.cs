using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Pairs;

namespace HelperPE.Application.Services
{
    public interface IStudentService
    {
        public Task SubmitApplicationToEvent(Guid eventId, Guid userId, string userRole);
        public Task<EventAttendanceStatusModel> CheckApplicationStatus(Guid eventId, Guid userId);
        public Task RestrictApplication(Guid eventId, Guid userId);

        public Task SubmitAttendanceToPair(Guid pairId, Guid userId);
        public Task<PairAttendanceStatusModel> CheckPairAttendanceStatus(Guid pairId, Guid userId);
        public Task RestrictPairAttendance(Guid pairId, Guid userId);
    }
}
