using HelperPE.Common.Models.Event;

namespace HelperPE.Application.Services
{
    public interface ISportsService
    {
        public Task<EventListModel> GetSportsOrgEventList(Guid sportsOrgId);
        public Task<EventModel> GetEventInfo(Guid eventId);
        public Task DeleteEvent(Guid eventId);
        public Task CreateEvent(Guid creatorId, EventCreateModel model);
    }
}
