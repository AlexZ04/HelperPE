using HelperPE.Persistence.Entities.Events;
using HelperPE.Persistence.Entities.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperPE.Persistence.Repositories
{
    public interface IFacultyRepository
    {
        public Task<FacultyEntity> GetFaculty (Guid facultyId);
    }
}
