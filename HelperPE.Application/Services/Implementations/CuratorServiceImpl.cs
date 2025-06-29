using HelperPE.Common.Constants;
using HelperPE.Common.Enums;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models;
using HelperPE.Common.Models.Attendances;
using HelperPE.Common.Models.Curator;
using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Events;
using HelperPE.Persistence.Entities.Users;
using HelperPE.Persistence.Extensions;
using HelperPE.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Application.Services.Implementations
{
    public class CuratorServiceImpl : ICuratorService
    {
        private readonly IProfileService _profileService;
        private readonly IUserRepository _userRepository;
        private readonly DataContext _context;

        public CuratorServiceImpl(
            IProfileService profileService,
            DataContext context,
            IUserRepository userRepository)
        {
            _profileService = profileService;
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<UserActivitiesModel> GetUserInfo(Guid userId)
        {
            var userActivities = await _profileService.GetUserActivities(userId);

            return userActivities;
        }

        public async Task EditEventApplicationStatus(Guid eventId, Guid userId, bool approve = true)
        {
            var application = await _context.EventsAttendances
                .FirstOrDefaultAsync(a => a.StudentId == userId && a.EventId == eventId);

            if (application == null)
                throw new DirectoryNotFoundException(ErrorMessages.APPLICATION_NOT_FOUND);

            if (application.Status == EventApplicationStatus.Pending)
                throw new BadRequestException(ErrorMessages.CAN_NOT_CHANGE_FIELD);

            if (application.Status == EventApplicationStatus.Credited && approve ||
                !approve && application.Status == EventApplicationStatus.Declined)
                throw new BadRequestException(ErrorMessages.ACTION_ALREADY_DONE);

            application.Status = approve ? EventApplicationStatus.Credited 
                : EventApplicationStatus.Declined;

            await _context.SaveChangesAsync();
        }

        public async Task<FacultiesModal> GetCuratorFaculties(Guid userId)
        {
            var user = await _profileService.GetCuratorProfileById(userId);

            return new FacultiesModal
            {
                Faculties = user.Faculties,
            };
        }

        public async Task<StudentsGroupModal> GetStudentsGroup(string groupNumber)
        {
            var users = await _context.Users
                .OfType<StudentEntity>()
                .Include(u => u.Faculty)
                .Include(u => u.Avatar)
                .Where(u => u.Group == groupNumber)
                .ToListAsync();

            if (users.Count == 0)
                throw new NotFoundException(ErrorMessages.GROUP_NOT_FOUND);

            return new StudentsGroupModal 
            {
                Faculty = users[0].Faculty.ToDto(),
                Group = groupNumber,
                Students = users.Select(u => u.ToShortDto()).ToList()
            };
        }

        public async Task<EventListModel> GetListOfEvents(Guid userId)
        {
            var user = await _profileService.GetCuratorProfileById(userId);

            var events = await GetListOfEventsFaculty(user.Faculties);

            return new EventListModel
            {
                Events = events.Select(e => e.ToDto()).ToList()
            };
        }

        private async Task<List<EventEntity>> GetListOfEventsFaculty(List<FacultyDTO> faculties)
        {
            var monthAgo = DateTime.UtcNow.AddMonths(-1);

            var events = await _context.Events
                .Include(e => e.Faculty)
                .Include(e => e.Attendances)
                    .ThenInclude(a => a.Student)
                        .ThenInclude(s => s.Avatar)
                .Where(e => faculties.Select(f => f.Id).Contains(e.Faculty.Id)
                            && e.Date >= monthAgo)
                .ToListAsync();

            return events;
        }

        public async Task<ApplicationsListModel> GetListOfEventsApplications(Guid userId)
        {
            var user = await _profileService.GetCuratorProfileById(userId);

            var eventsList = await GetListOfEventsFaculty(user.Faculties);

            var attendances = eventsList.Select(e => e.Attendances).ToList();

            var validAttendances = new List<EventAttendanceFullModel>();

            foreach (var attendance in attendances)
            {
                validAttendances.AddRange(attendance
                    .Where(a => a.Status == EventApplicationStatus.Accepted)
                    .Select(a => a.ToFullDto())
                    .ToList());
            }

            return new ApplicationsListModel
            {
                Attendances = validAttendances,
            };
        }

        public async Task CreateOtherActivity(
            OtherActivityCreateModel activity,
            Guid studentId,
            Guid curatorId)
        {
            var curator = await _userRepository.GetCuratorById(curatorId);
            var student = await _userRepository.GetStudentById(studentId);

            var newActivity = activity.CreateOtherActivity(curator, student);

            student.OtherActivities.Add(newActivity);
            _context.OtherActivities.Add(newActivity);
            await _context.SaveChangesAsync();
        }

        public async Task AddSportOrg(Guid studentId)
        {
            var student = await _userRepository.GetStudentById(studentId);
            student.Role = UserRole.SportsOrganizer;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteSportOrg(Guid sportOrgId)
        {
            var sportOrg = await _userRepository.GetSportsById(sportOrgId);
            sportOrg.Role = UserRole.Student;
            await _context.SaveChangesAsync();
        }

        public async Task<List<StudentProfileShortModel>> GetStudents()
        {
            var students = await _context.Users.OfType<StudentEntity>().Include(u => u.Faculty).Include(u => u.Avatar).ToListAsync();
            var studentDtos = students.Select(u => u.ToShortDto()).ToList();
            studentDtos = studentDtos.OrderBy(u => u.Name).ToList();

            return studentDtos;
        }
    }
}
