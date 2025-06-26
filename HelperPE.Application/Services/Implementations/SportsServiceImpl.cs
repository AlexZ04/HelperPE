using HelperPE.Common.Constants;
using HelperPE.Common.Enums;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Event;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Events;
using HelperPE.Persistence.Extensions;
using HelperPE.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task<EventFullModel> GetEventInfo(Guid eventId)
        {
            var foundEvent = await _eventRepository.GetEvent(eventId);

            return foundEvent.ToFullDto();
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

        public async Task EditEventApplicationStatus(Guid eventId, Guid userId, SportsOrgEventStatus status)
        {
            var application = await _context.EventsAttendances
                .FirstOrDefaultAsync(a => a.StudentId == userId && a.EventId == eventId);

            if (application == null)
                throw new NotFoundException(ErrorMessages.APPLICATION_NOT_FOUND);

            if (application.Status == EventApplicationStatus.Credited)
                throw new BadRequestException(ErrorMessages.CAN_NOT_CHANGE_FIELD);

            EventApplicationStatus newStatus = EventApplicationStatus.Pending;

            if (status == SportsOrgEventStatus.Accepted)
                newStatus = EventApplicationStatus.Accepted;
            else if (status == SportsOrgEventStatus.Declined)
                newStatus = EventApplicationStatus.Declined;

            application.Status = newStatus;
            await _context.SaveChangesAsync();
        }

    }
}
