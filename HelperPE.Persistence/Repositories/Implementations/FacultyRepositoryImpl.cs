using HelperPE.Common.Constants;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Faculty;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperPE.Persistence.Repositories.Implementations
{
    public class FacultyRepositoryImpl : IFacultyRepository
    {
        private readonly DataContext _context;

        public FacultyRepositoryImpl(DataContext context)
        {
            _context = context;
        }
        
        public async Task<FacultyEntity> GetFaculty(Guid facultyId)
        {
            var faculty = await _context.Faculties.FirstOrDefaultAsync(el => el.Id == facultyId);
            if (faculty == null) { throw new NotFoundException(ErrorMessages.FACULTY_NOT_FOUND); }
            return faculty;
        }
    }
}
