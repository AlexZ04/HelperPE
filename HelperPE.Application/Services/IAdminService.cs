using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Entities.Faculty;

namespace HelperPE.Application.Services
{
    public interface IAdminService
    {
        public Task AddСurator(Guid teacherId, Guid facultyId);
        public Task DeleteСurator(Guid curatorId, Guid facultyId);
        public Task<List<FacultyEntity>> GetFaculties();
        public Task<List<CuratorProfileDTO>> GetCurators();
        public Task<List<TeacherProfileDTO>> GetTeachers();
    }
}
