using HelperPE.Common.Models.Teacher;

namespace HelperPE.Application.Services
{
    public interface ITeacherService
    {
        public Task<SubjectListModel> GetTeacherSubjects(Guid teacherId);
        public Task<TeacherPairsModel> GetTodayPairs(Guid teacherId);
    }
}
