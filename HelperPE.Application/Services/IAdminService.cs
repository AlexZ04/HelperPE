using HelperPE.Common.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperPE.Application.Services
{
    public interface IAdminService
    {
        //public Task AddСurator(Guid teacherId, Guid facultyId);
        public Task<List<CuratorProfileDTO>> GetCurators();
        public Task<List<TeacherProfileDTO>> GetTeachers();
    }
}
