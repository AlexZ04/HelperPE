using HelperPE.Common.Enums;
using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Repositories
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUsersByCredentials(string email, string password);
        public Task<StudentEntity> GetStudentById(Guid userId);
        public Task<SportsOrganizerEntity> GetSportsById(Guid userId);
        public Task<TeacherEntity> GetTeacherById(Guid userId);
        public Task<CuratorEntity> GetCuratorById(Guid userId);
    }
}
