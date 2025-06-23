using HelperPE.Common.Constants;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperPE.Application.Services.Implementations
{
    public class UserDBManipulatingImpl : IUserDBManipulating
    {
        private readonly DataContext _context;
        private readonly IUserDBManipulating _userDBManipulating;
        public UserDBManipulatingImpl(DataContext context, IUserDBManipulating userDBManipulating)
        {
            _context = context;
            _userDBManipulating = userDBManipulating;
        }

        public async Task<CuratorProfileDTO> AddCurator(Guid teacherId, Guid FacultyId)
        {
            var teacher = await GetTeacherById(teacherId);
            throw new NotImplementedException();
        }

        public Task<CuratorProfileDTO> DeleteCurator(Guid teacherId, Guid FacultyId)
        {
            throw new NotImplementedException();
        }

        private async Task<CuratorEntity> GetCuratorById(Guid id)
        {
            var curator = await _context.Users
            .OfType<CuratorEntity>()
            .FirstOrDefaultAsync(t => t.Id == id);

            if (curator == null)
                throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);
            return curator;
        }

        private async Task<TeacherEntity> GetTeacherById(Guid id)
        {
            var teacher = await _context.Users
            .OfType<TeacherEntity>()
            .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null)
                throw new NotFoundException(ErrorMessages.USER_NOT_FOUND);
            return teacher;
        }
    }
}
