using HelperPE.Common.Models.Event;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Repositories;
using HelperPE.Persistence.Extensions;
using HelperPE.Persistence.Entities.Events;

namespace HelperPE.Application.Services.Implementations
{
    public class SportsServiceImpl : ISportsService
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;

        public SportsServiceImpl(
            DataContext context,
            IUserRepository userRepository,
            IEventRepository eventRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
        }

        public async Task<EventListModel> GetSportsOrgEventList(Guid sportsOrgId)
        {
            var sportsOrg = await _userRepository.GetSportsById(sportsOrgId);

            return new EventListModel
            {
                Events = sportsOrg.Events.Select(e => e.ToDto()).ToList(),
            };
        }

        public async Task<EventModel> GetEventInfo(Guid eventId)
        {
            var foundEvent = await _eventRepository.GetEvent(eventId);

            return foundEvent.ToDto();
        }

        public async Task DeleteEvent(Guid eventId)
        {
            var foundEvent = await _eventRepository.GetEvent(eventId);

            _context.Events.Remove(foundEvent);
            await _context.SaveChangesAsync();
        }

        public async Task CreateEvent(Guid creatorId, EventCreateModel model)
        {
            var user = await _userRepository.GetSportsById(creatorId);

            var eventEntity = model.CreateEvent(user.Faculty);
            user.Events.Add(eventEntity);

            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();
        }
    }
}
