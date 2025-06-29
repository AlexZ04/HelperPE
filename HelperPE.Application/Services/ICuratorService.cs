using HelperPE.Common.Enums;
using HelperPE.Common.Models.Attendances;
using HelperPE.Common.Models.Curator;
using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

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
        public Task CreateOtherActivity(OtherActivityCreateModel activity, Guid studentId, Guid curatorId);
        public Task AddSportOrg(Guid studentId);
        public Task DeleteSportOrg(Guid sportOrgId);
    }
}
