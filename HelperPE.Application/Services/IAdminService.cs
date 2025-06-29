using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Entities.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperPE.Application.Services
{
    public interface IAdminService
    {
        public Task<CuratorProfileDTO> AddСurator(Guid teacherId, Guid facultyId);
        public Task DeleteСurator(Guid curatorId, Guid facultyId);
        public Task<List<FacultyEntity>> GetFaculties();
        public Task<List<CuratorProfileDTO>> GetCurators();
        public Task<List<TeacherProfileDTO>> GetTeachers();
    }
}
