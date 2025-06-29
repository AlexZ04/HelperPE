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
                var teacher = await _userRepository.GetCuratorById(userId);
                teacher.Faculties.Add(faculty);
            }
            else
            {
                var teacher = await _userRepository.GetTeacherById(userId);
                
                CuratorEntity curator = new CuratorEntity()
                {
                    Id = teacher.Id,
                    Email = teacher.Email,
                    FullName = teacher.FullName,
                    Role = UserRole.Curator,
                    Password = teacher.Password,
                    Avatar = teacher.Avatar,
                    Subjects = teacher.Subjects,
                    Pairs = teacher.Pairs,
                    Faculties = new List<FacultyEntity> { faculty }
                };
                
                _context.Users.Remove(teacher);
                _context.Users.Add(curator);
            }

            var currentCurator = await GetFacultyCurator(facultyId, userId);
            if (currentCurator != null)
            {
                currentCurator.Faculties.Remove(faculty);

                if (currentCurator.Faculties.Count == 0)
                {
                    TeacherEntity teacher = new TeacherEntity()
                    {
                        Id = currentCurator.Id,
                        Email = currentCurator.Email,
                        FullName = currentCurator.FullName,
                        Role = UserRole.Teacher,
                        Password = currentCurator.Password,
                        Avatar = currentCurator.Avatar,
                        Subjects = currentCurator.Subjects,
                        Pairs = currentCurator.Pairs
                    };
                    
                    _context.Users.Remove(currentCurator);
                    _context.Users.Add(teacher);
                }
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
                    TeacherEntity teacher = new TeacherEntity()
                    {
                        Id = curator.Id,
                        Email = curator.Email,
                        FullName = curator.FullName,
                        Role = UserRole.Teacher,
                        Password = curator.Password,
                        Avatar = curator.Avatar,
                        Subjects = curator.Subjects,
                        Pairs = curator.Pairs
                    };
                    _context.Users.Remove(curator);
                    await _context.AddAsync(teacher);
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
