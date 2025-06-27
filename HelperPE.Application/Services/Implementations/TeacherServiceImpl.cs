using HelperPE.Common.Models.Teacher;
using HelperPE.Persistence.Extensions;
using HelperPE.Persistence.Repositories;

namespace HelperPE.Application.Services.Implementations
{
    public class TeacherServiceImpl : ITeacherService
    {
        private readonly IProfileService _profileService;
        private readonly IUserRepository _userRepository;

        public TeacherServiceImpl(
            IProfileService profileService,
            IUserRepository userRepository)
        {
            _profileService = profileService;
            _userRepository = userRepository;
        }

        public async Task<SubjectListModel> GetTeacherSubjects(Guid teacherId)
        {
            var teacher = await _profileService.GetTeacherProfileById(teacherId);

            return new SubjectListModel
            {
                Subjects = teacher.Subjects
            };
        }

        public async Task<TeacherPairsModel> GetTodayPairs(Guid teacherId)
        {
            var teacher = await _userRepository.GetTeacherById(teacherId);

            var todayPairs = teacher.Pairs
                .Where(p => p.Date == DateTime.Today)
                .ToList();

            return new TeacherPairsModel
            {
                Pairs = todayPairs.Select(p => p.ToDto()).ToList()
            };
        }
    }
}
