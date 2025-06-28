using HelperPE.Common.Models.Teacher;

namespace HelperPE.Application.Services
{
    public interface ITeacherService
    {
        public Task<SubjectListModel> GetTeacherSubjects(Guid teacherId);
        public Task<TeacherPairsModel> GetTodayPairs(Guid teacherId);
        public Task CreatePair(Guid subjectId, Guid teacherId);
        public Task<PairAttendancesListModel> GetPairAttendances(Guid teacherId);
        public Task<PairAttendancesListModel> GetPairAttendances(Guid pairId, Guid teacherId);
        public Task EditPairAttendanceStatus(Guid pairId, Guid userId, int classesAmount = 1, bool approve = true);
    }
}
