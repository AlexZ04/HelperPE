using HelperPE.Common.Models.Profile;

namespace HelperPE.Application.Services
{
    public interface IProfileService
    {
        public Task<StudentProfileDTO> GetStudentProfileById(Guid id);
        public Task<SportsOrganizerProfileDTO> GetSportsProfileById(Guid id);
        public Task<TeacherProfileDTO> GetTeacherProfileById(Guid id);
        public Task<CuratorProfileDTO> GetCuratorProfileById(Guid id);
        public Task<List<CuratorProfileDTO>> GetCurators();
        public Task<List<TeacherProfileDTO>> GetTeachers();
        public Task<UserActivitiesModel> GetUserActivities(Guid id);
    }
}
