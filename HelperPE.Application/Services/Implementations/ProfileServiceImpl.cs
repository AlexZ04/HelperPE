using HelperPE.Common.Constants;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Users;
using HelperPE.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Application.Services.Implementations
{
    public class ProfileServiceImpl : IProfileService
    {
        private readonly DataContext _context;

        public ProfileServiceImpl(DataContext context)
        {
            _context = context;
        }

        public async Task<StudentProfileDTO> GetStudenProfileById(Guid id)
        {
            var student = await _context.Users
                .OfType<StudentEntity>()
                .Include(u => u.Faculty)
                .Include(u => u.PairAttendances)
                    .ThenInclude(a => a.Pair)
                .Include(u => u.OtherActivities)
                    .ThenInclude(a => a.Teacher)
                .Include(u => u.EventsAttendances)
                    .ThenInclude(a => a.Event)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (student == null)
                throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);

            return student.ToDto();
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
    }
}
