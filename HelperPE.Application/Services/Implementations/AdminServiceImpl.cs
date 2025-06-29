using HelperPE.Common.Constants;
using HelperPE.Common.Enums;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Faculty;
using HelperPE.Persistence.Entities.Users;
using HelperPE.Persistence.Extensions;
using HelperPE.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperPE.Application.Services.Implementations
{
    public class AdminServiceImpl : IAdminService
    {
        private readonly DataContext _context;
        private readonly IProfileService _profileServiceImpl;
        private readonly IUserRepository _userRepository;
        private readonly IFacultyRepository _facultyRepository;
        public AdminServiceImpl(DataContext context, IProfileService profileServiceImpl, IUserRepository userRepository, IFacultyRepository facultyRepository)
        {
            _context = context;
            _profileServiceImpl = profileServiceImpl;
            _userRepository = userRepository;
            _facultyRepository = facultyRepository;
        }



        public async Task<CuratorProfileDTO> AddСurator(Guid userId, Guid facultyId)
        {
            var teacher = await GetTeacherById(userId);
            var faculty = await _facultyRepository.GetFaculty(facultyId);
            if ( teacher != null)
            {
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
                    Faculties = [faculty]
                };
                _context.Users.Remove(teacher);
                await _context.AddAsync(curator);    
                await _context.SaveChangesAsync();
                return curator.ToDto();
            }
            else
            {
                var curator = await _userRepository.GetCuratorById(userId);
                if (curator == null) { throw new NotFoundException(ErrorMessages.USER_NOT_FOUND); }
                curator.Faculties.Add(faculty);
                await _context.SaveChangesAsync();
                return curator.ToDto();
            }

        }

        public async Task DeleteСurator(Guid curatorId, Guid facultyId)
        {
            var curator = await _userRepository.GetCuratorById(curatorId);
            if (curator == null) { throw new NotFoundException(ErrorMessages.USER_NOT_FOUND); }
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

        private async Task<TeacherEntity> GetTeacherById(Guid id)
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
            .FirstOrDefaultAsync(t => t.Id == id);
            return teacher;
        }



    }
}
