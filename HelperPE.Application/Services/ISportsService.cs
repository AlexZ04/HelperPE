using HelperPE.Common.Models.Event;

namespace HelperPE.Application.Services
{
    public interface ISportsService
    {
        public Task<EventListModel> GetSportsOrgEventList(Guid sportsOrgId);
    }
}
