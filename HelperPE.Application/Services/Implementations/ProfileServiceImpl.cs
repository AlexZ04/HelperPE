using HelperPE.Common.Constants;
using HelperPE.Common.Enums;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Attendances;
using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Users;
using HelperPE.Persistence.Extensions;
using HelperPE.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Application.Services.Implementations
{
    public class ProfileServiceImpl : IProfileService
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;

        public ProfileServiceImpl(
            DataContext context,
            IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<StudentProfileDTO> GetStudentProfileById(Guid id)
        {
            var student = await _userRepository.GetStudentById(id);

            return student.ToDto();
        }

        public async Task<SportsOrganizerProfileDTO> GetSportsProfileById(Guid id)
        {
            var sports = await _userRepository.GetSportsById(id);

            return sports.ToDto();
        }

        public async Task<TeacherProfileDTO> GetTeacherProfileById(Guid id)
        {
            var teacher = await _context.Users
                .OfType<TeacherEntity>()
                .Include(t => t.Pairs)
                .Include(t => t.Subjects)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
                throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);

            return teacher.ToDto();
        }

        public async Task<CuratorProfileDTO> GetCuratorProfileById(Guid id)
        {
            var curator = await _context.Users
                .OfType<CuratorEntity>()
                .Include(t => t.Pairs)
                .Include(t => t.Subjects)
                .Include(t => t.Faculties)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (curator == null)
                throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);

            return curator.ToDto();
        }

        public async Task<List<CuratorProfileDTO>> GetCurators()
        {
            var curators = await _context.Users
            .OfType<CuratorEntity>()
            //.Include(t => t.Pairs)
            .Include(t => t.Subjects)
            .Include(t => t.Faculties)
            .ToListAsync();

            return curators.Select(c => c.ToDto()).ToList();
        }

        public async Task<List<TeacherProfileDTO>> GetTeachers()
        {
            var teachers = await _context.Users
            .OfType<TeacherEntity>()
            //.Include(t => t.Pairs)
            .Include(t => t.Subjects)
            .ToListAsync();
     
            return teachers.Select(c => c.ToDto()).ToList();
        }

        public async Task<UserActivitiesModel> GetUserActivities(Guid id)
        {
            var student = await _userRepository.GetStudentById(id);

            var activitiesModel = new UserActivitiesModel
            {
                Student = student.ToDto(),
                Pairs = student.PairAttendances
                    .Select(a => a.ToDto()).ToList(),
                Events = student.EventsAttendances
                    .Select(a => a.ToDto()).ToList(),
                OtherActivities = student.OtherActivities
                    .Select(a => a.ToDto()).ToList()
            };

            return activitiesModel;
        }

        public async Task<EventListModel> GetSportsOrgEventList(Guid sportsOrgId)
        {
            var sportsOrg = await _userRepository.GetSportsById(sportsOrgId);

            return new EventListModel
            {
                Events = sportsOrg.Events.Select(e => e.ToDto()).ToList(),  
            };
        }
    }
}
