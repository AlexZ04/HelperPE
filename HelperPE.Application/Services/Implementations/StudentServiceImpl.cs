using HelperPE.Common.Constants;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Event;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Events;
using HelperPE.Persistence.Repositories;

namespace HelperPE.Application.Services.Implementations
{
    public class StudentServiceImpl : IStudentService
    {
        private readonly DataContext _context; 
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;

        public StudentServiceImpl(
            DataContext context, 
            IUserRepository userRepository,
            IEventRepository eventRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
        }

        public async Task SubmitApplicationToEvent(Guid eventId, Guid userId, string userRole)
        {
            var foundEvent = await _eventRepository.GetEvent(eventId);
            var user = await _userRepository.GetStudentById(userId);

            if (userRole == RolesCombinations.SPORTS)
                user = await _userRepository.GetStudentById(userId);

            if (user.EventsAttendances.Select(e => e.StudentId).Contains(userId))
                throw new BadRequestException(ErrorMessages.USER_ALREADY_HAS_APPLICATION);

            var newEventAttendance = new EventAttendanceEntity
            {
                EventId = eventId,
                StudentId = userId,
                Event = foundEvent,
                Student = user,
            };

            _context.EventsAttendances.Add(newEventAttendance);
            user.EventsAttendances.Add(newEventAttendance);
            foundEvent.Attendances.Add(newEventAttendance);

            await _context.SaveChangesAsync();
        }

        public async Task<EventAttendanceStatusModel> CheckApplicationStatus(Guid eventId, Guid userId)
        {
            var foundEvent = await _eventRepository.GetEvent(eventId);

            var application = GetEventApplicationEntity(foundEvent, userId);

            return new EventAttendanceStatusModel { 
                EventApplicationStatus = application.Status
            };
        }

        public async Task RestrictApplication(Guid eventId, Guid userId)
        {
            var foundEvent = await _eventRepository.GetEvent(eventId);
            var user = await _userRepository.GetStudentById(userId);

            var application = GetEventApplicationEntity(foundEvent, userId);

            user.EventsAttendances.Remove(application);
            foundEvent.Attendances.Remove(application);

            _context.EventsAttendances.Remove(application);

            await _context.SaveChangesAsync();
        }

        private EventAttendanceEntity GetEventApplicationEntity(EventEntity foundEvent, Guid userId)
        {
            var application = foundEvent.Attendances
                .FirstOrDefault(u => u.StudentId == userId);

            if (application == null)
                throw new NotFoundException(ErrorMessages.APPLICATION_NOT_FOUND);

            return application;
        }

    }
}
