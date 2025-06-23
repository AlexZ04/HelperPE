using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperPE.Application.Services
{
    public interface IUserDBManipulating
    {

        public Task<CuratorProfileDTO> AddCurator(Guid teacherId, Guid FacultyId);
        public Task<CuratorProfileDTO> DeleteCurator(Guid teacherId, Guid FacultyId);
    }
}
