using HelperPE.Common.Constants;
using HelperPE.Common.Enums;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models;
using HelperPE.Common.Models.Curator;
using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Events;
using HelperPE.Persistence.Entities.Users;
using HelperPE.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HelperPE.Application.Services.Implementations
{
    public class CuratorServiceImpl : ICuratorService
    {
        private readonly IProfileService _profileService;
        private readonly DataContext _context;

        public CuratorServiceImpl(
            IProfileService profileService,
            DataContext context)
        {
            _profileService = profileService;
            _context = context;
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

            var events = await GetListOfEvents(user.Faculties);

            return new EventListModel
            {
                Events = events.Select(e => e.ToDto()).ToList()
            };
        }

        private async Task<List<EventEntity>> GetListOfEvents(List<FacultyDTO> faculties)
        {
            var events = await _context.Events
                .Include(e => e.Faculty)
                .Include(e => e.Attendances)
                    .ThenInclude(a => a.Student)
                .Where(e => faculties.Select(f => f.Id).Contains(e.Faculty.Id))
                .ToListAsync();

            return events;
        }
    }
}
