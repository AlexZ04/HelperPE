using HelperPE.Application.Notifications.NotificationSender;
using HelperPE.Common.Constants;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models;
using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Pairs;
using HelperPE.Infrastructure.Utilities;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Events;
using HelperPE.Persistence.Entities.Pairs;
using HelperPE.Persistence.Extensions;
using HelperPE.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Application.Services.Implementations
{
    public class StudentServiceImpl : IStudentService
    {
        private readonly DataContext _context; 
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IPairRepository _pairRepository;
        private readonly IWebSocketNotificationService _notificationService;

        public StudentServiceImpl(
            DataContext context, 
            IUserRepository userRepository,
            IEventRepository eventRepository,
            IPairRepository pairRepository,
            IWebSocketNotificationService notificationService)
        {
            _context = context;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _pairRepository = pairRepository;
            _notificationService = notificationService;
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

            return application ?? throw new NotFoundException(ErrorMessages.APPLICATION_NOT_FOUND);
        }

        public async Task SubmitAttendanceToPair(Guid pairId, Guid userId)
        {
            var user = await _userRepository.GetStudentById(userId);
            var pair = await _pairRepository.GetPair(pairId);

            var currentPairNumber = TimeUtility.GetPairNumber();

            if (user.PairAttendances.Select(e => e.StudentId).Contains(userId))
                throw new BadRequestException(ErrorMessages.USER_ALREADY_HAS_APPLICATION);

            if (pair.PairNumber != currentPairNumber)
                throw new BadRequestException(ErrorMessages.YOU_ARE_LATE);

            var newAttendance = new PairAttendanceEntity
            {
                PairId = pairId,
                StudentId = userId,
                Student = user,
                Pair = pair
            };

            pair.Attendances.Add(newAttendance);
            user.PairAttendances.Add(newAttendance);

            _context.PairsAttendances.Add(newAttendance);
            await _context.SaveChangesAsync();

            var notificationObject = newAttendance.ToProfileDto();

            _notificationService.NotifyPairAttendanceSubmitted(notificationObject);
        }

        public async Task<PairAttendanceStatusModel> CheckPairAttendanceStatus(Guid pairId, Guid userId)
        {
            var pair = await _pairRepository.GetPair(pairId);

            var attendance = GetPairAttendanceEntity(pair, userId);

            return new PairAttendanceStatusModel
            {
                Status = attendance.Status,
            };
        }

        public async Task RestrictPairAttendance(Guid pairId, Guid userId)
        {
            var user = await _userRepository.GetStudentById(userId);
            var pair = await _pairRepository.GetPair(pairId);

            var attendance = GetPairAttendanceEntity(pair, userId);

            var notificationObject = attendance.ToProfileDto();
            _notificationService.NotifyPairAttendanceDeleted(notificationObject);

            pair.Attendances.Remove(attendance);
            user.PairAttendances.Remove(attendance);

            _context.PairsAttendances.Remove(attendance);
            await _context.SaveChangesAsync();
        }

        private PairAttendanceEntity GetPairAttendanceEntity(PairEntity pair, Guid userId)
        {
            var attendance = pair.Attendances
                                .FirstOrDefault(a => a.StudentId == userId);

            return attendance ?? throw new NotFoundException(ErrorMessages.ATTENDANCE_NOT_FOUND);
        }

        public async Task<PairListModel> GetAvailablePairs(Guid userId)
        {
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            var currentPairNumber = TimeUtility.GetPairNumber();

            var todayPairs = await _context.Pairs
                .Include(p => p.Teacher)
                .Include(p => p.Subject)
                .Where(p => p.Date >= today && p.Date < tomorrow && p.PairNumber == currentPairNumber)
                .ToListAsync();

            return new PairListModel
            {
                Pairs = todayPairs.Select(p => p.ToShortDto()).ToList(),   
            };
        }

        public async Task<EventListModel> GetAvailableEvents(Guid userId)
        {
            var user = await _userRepository.GetStudentById(userId);
            var availableEvents = await GetListOfEventsFaculty(user.Faculty.ToDto());

            return new EventListModel
            {
                Events = availableEvents.Select(e => e.ToDto()).ToList(),
            };
        }

        private async Task<List<EventEntity>> GetListOfEventsFaculty(FacultyDTO faculty)
        {
            var monthAgo = DateTime.UtcNow.AddMonths(-1);

            var events = await _context.Events
                .Include(e => e.Faculty)
                .Include(e => e.Attendances)
                    .ThenInclude(a => a.Student)
                .Where(e => faculty.Id == e.Faculty.Id && e.Date >= monthAgo)
                .ToListAsync();

            return events;
        }
    }
}
