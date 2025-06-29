using HelperPE.Common.Constants;
using HelperPE.Common.Enums;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Faculty;
using HelperPE.Persistence.Entities.Users;
using HelperPE.Persistence.Extensions;
using HelperPE.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Application.Services.Implementations
{
    public class AdminServiceImpl : IAdminService
    {
        private readonly DataContext _context;
        private readonly IProfileService _profileServiceImpl;
        private readonly IUserRepository _userRepository;
        private readonly IFacultyRepository _facultyRepository;

        public AdminServiceImpl(DataContext context,
            IProfileService profileServiceImpl,
            IUserRepository userRepository,
            IFacultyRepository facultyRepository)
        {
            _context = context;
            _profileServiceImpl = profileServiceImpl;
            _userRepository = userRepository;
            _facultyRepository = facultyRepository;
        }

        
        public async Task AddСurator(Guid userId, Guid facultyId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);

            var faculty = await _facultyRepository.GetFaculty(facultyId);

            if (user.Role == UserRole.Curator)
            {
                var curator = await _userRepository.GetCuratorById(userId);
                if (!curator.Faculties.Any(f => f.Id == faculty.Id))
                {
                    curator.Faculties.Add(faculty);
                }
                else { throw new BadRequestException(ErrorMessages.CURATOR_ALREADY_HAS_THIS_FACULTY); }
            }
            else
            {
                var teacher = await _userRepository.GetTeacherById(userId);
                teacher.Role = UserRole.Curator;
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();

                var newCurator = await _userRepository.GetCuratorById(userId);
                newCurator.Faculties.Add(faculty);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteСurator(Guid curatorId, Guid facultyId)
        {
            var curator = await _userRepository.GetCuratorById(curatorId);

            var facultyToRemove = curator.Faculties.FirstOrDefault(f => f.Id == facultyId);

            if (facultyToRemove != null)
            {
                curator.Faculties.Remove(facultyToRemove); 
                _context.Users.Update(curator); 
                await _context.SaveChangesAsync(); 
                if(curator.Faculties.Count() == 0)
                {
                    var exCurator = await _userRepository.GetCuratorById(curatorId);
                    exCurator.Role = UserRole.Teacher;
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                throw new NotFoundException($"Faculty with id {facultyId} not found for curator with id {curatorId}");
            }
        }

        public async Task<List<CuratorProfileDTO>> GetCurators()
        {
            var curators = await _profileServiceImpl.GetCurators();
            return (curators);
        }

        public async Task<List<TeacherProfileDTO>> GetTeachers()
        {
            var teachers = await _profileServiceImpl.GetTeachers();
            return (teachers);
        }

        public async Task<List<FacultyEntity>> GetFaculties()
        {
            var faculties = await _context.Faculties.ToListAsync();
            return faculties;
        }

        private async Task<CuratorEntity?> GetFacultyCurator(Guid facultyId, Guid userId)
        {
            var curator = await _context.Users
                            .OfType<CuratorEntity>()
                            .Include(c => c.Pairs)
                                .ThenInclude(p => p.Subject)
                            .Include(c => c.Pairs)
                                .ThenInclude(p => p.Attendances)
                                    .ThenInclude(a => a.Student)
                            .Include(c => c.Subjects)
                            .Include(u => u.Avatar)
                            .Include(c => c.Faculties)
                            .FirstOrDefaultAsync(c => c.Faculties.Any(f => f.Id == facultyId) && c.Id != userId);

            return curator;
        }
    }
}
