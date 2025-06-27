using HelperPE.Common.Models.Teacher;

namespace HelperPE.Application.Services
{
    public interface ITeacherService
    {
        public Task<SubjectListModel> GetTeacherSubjects(Guid teacherId);
        public Task<TeacherPairsModel> GetTodayPairs(Guid teacherId);
        public Task CreatePair(Guid subjectId, Guid teacherId);
        public Task<PairAttendancesListModel> GetPairAttendances(Guid teacherId);
    }
}
