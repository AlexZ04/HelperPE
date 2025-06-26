using HelperPE.Common.Models.Attendances;
using HelperPE.Common.Models.Curator;
using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Profile;

namespace HelperPE.Application.Services
{
    public interface ICuratorService
    {
        public Task<UserActivitiesModel> GetUserInfo(Guid userId);
        public Task EditEventApplicationStatus(Guid eventId, Guid userId, bool approve = true);
        public Task<FacultiesModal> GetCuratorFaculties(Guid userId);
        public Task<StudentsGroupModal> GetStudentsGroup(string groupNumber);
        public Task<EventListModel> GetListOfEvents(Guid userId);
        public Task<ApplicationsListModel> GetListOfEventsApplications(Guid userId);
    }
}
