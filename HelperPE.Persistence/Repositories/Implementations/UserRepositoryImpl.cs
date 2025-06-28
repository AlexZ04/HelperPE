using HelperPE.Common.Constants;
using HelperPE.Common.Enums;
using HelperPE.Common.Exceptions;
using HelperPE.Infrastructure.Utilities;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Persistence.Repositories.Implementations
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly DataContext _context;
        
        public UserRepositoryImpl(DataContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetUsersByCredentials(string email, string password)
        {
            UserEntity? user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !Hasher.CheckPassword(user.Password, password))
                throw new BadRequestException(ErrorMessages.INVALID_CREDENTIALS);

            return user;
        }

        public async Task<UserEntity> GetUserById(Guid userId)
        {
            var user = await _context.Users
                .OfType<UserEntity>()
                .Include(u => u.Avatar)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);

            return user;
        }


        public async Task<StudentEntity> GetStudentById(Guid userId)
        {
            var user = await _context.Users
                .OfType<StudentEntity>()
                .Include(u => u.Faculty)
                .Include(u => u.PairAttendances)
                    .ThenInclude(a => a.Pair)
                        .ThenInclude(p => p.Teacher)
                .Include(u => u.PairAttendances)
                    .ThenInclude(a => a.Pair)
                        .ThenInclude(p => p.Subject)
                .Include(u => u.OtherActivities)
                    .ThenInclude(a => a.Teacher)
                .Include(u => u.EventsAttendances)
                    .ThenInclude(a => a.Event)
                .Include(u => u.Avatar)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);

            return user;
        }

        public async Task<SportsOrganizerEntity> GetSportsById(Guid userId)
        {
            var user = await _context.Users
                .OfType<SportsOrganizerEntity>()
                .Include(u => u.Faculty)
                .Include(u => u.PairAttendances)
                    .ThenInclude(a => a.Pair)
                .Include(u => u.OtherActivities)
                    .ThenInclude(a => a.Teacher)
                .Include(u => u.EventsAttendances)
                    .ThenInclude(a => a.Event)
                .Include(s => s.Events)
                    .ThenInclude(s => s.Faculty)
                .Include(s => s.Events)
                    .ThenInclude(e => e.Attendances)
                .Include(u => u.Avatar)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) 
                throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);

            return user;
        }

        public async Task<TeacherEntity> GetTeacherById(Guid userId)
        {
            var teacher = await _context.Users
                .OfType<TeacherEntity>()
                .Include(t => t.Pairs)
                    .ThenInclude(p => p.Subject)
                .Include(t => t.Pairs)
                    .ThenInclude(p => p.Attendances)
                        .ThenInclude(a => a.Student)
                .Include(t => t.Subjects)
                .Include(u => u.Avatar)
                .FirstOrDefaultAsync(t => t.Id == userId);

            if (teacher == null)
                throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);

            return teacher;
        }

        public async Task<CuratorEntity> GetCuratorById(Guid userId)
        {
            var curator = await _context.Users
                .OfType<CuratorEntity>()
                .Include(t => t.Pairs)
                    .ThenInclude(p => p.Subject)
                .Include(t => t.Subjects)
                .Include(t => t.Faculties)
                .Include(u => u.Avatar)
                .FirstOrDefaultAsync(t => t.Id == userId);

            if (curator == null)
                throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);

            return curator;
        }
    }
}
