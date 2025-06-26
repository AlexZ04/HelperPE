using HelperPE.Persistence.Entities.Events;

namespace HelperPE.Persistence.Repositories
{
    public interface IEventRepository
    {
        public Task<EventEntity> GetEvent(Guid eventId);
    }
}
