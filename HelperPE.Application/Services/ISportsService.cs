using HelperPE.Common.Enums;
using HelperPE.Common.Models.Event;

namespace HelperPE.Application.Services
{
    public interface ISportsService
    {
        public Task<EventListModel> GetSportsOrgEventList(Guid sportsOrgId);
        public Task<EventFullModel> GetEventInfo(Guid eventId);
        public Task DeleteEvent(Guid eventId);
        public Task CreateEvent(Guid creatorId, EventCreateModel model);
        public Task EditEventApplicationStatus(Guid eventId, Guid userId, SportsOrgEventStatus status);
    }
}
