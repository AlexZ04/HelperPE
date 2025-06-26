using HelperPE.Common.Models.Event;

namespace HelperPE.Application.Services
{
    public interface IStudentService
    {
        public Task SubmitApplicationToEvent(Guid eventId, Guid userId, string userRole);
        public Task<EventAttendanceStatusModel> CheckApplicationStatus(Guid eventId, Guid userId);
        public Task RestrictApplication(Guid eventId, Guid userId);
    }
}
