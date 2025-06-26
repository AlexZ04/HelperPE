using HelperPE.Common.Constants;
using HelperPE.Common.Exceptions;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Events;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Persistence.Repositories.Implementations
{
    public class EventRepositoryImpl : IEventRepository
    {
        private readonly DataContext _context;

        public EventRepositoryImpl(DataContext context)
        {
            _context = context;
        }

        public async Task<EventEntity> GetEvent(Guid eventId)
        {
            var foundEvent = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == eventId);

            if (foundEvent == null)
                throw new NotFoundException(ErrorMessages.EVENT_NOT_FOUND);

            return foundEvent;
        }
    }
}
