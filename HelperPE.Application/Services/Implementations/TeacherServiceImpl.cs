using HelperPE.Common.Models.Teacher;

namespace HelperPE.Application.Services.Implementations
{
    public class TeacherServiceImpl : ITeacherService
    {
        private readonly IProfileService _profileService;

        public TeacherServiceImpl(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<SubjectListModel> GetTeacherSubjects(Guid teacherId)
        {
            var teacher = await _profileService.GetTeacherProfileById(teacherId);

            return new SubjectListModel
            {
                Subjects = teacher.Subjects
            };
        }
    }
}
