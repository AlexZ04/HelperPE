using HelperPE.Common.Constants;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Teacher;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Pairs;
using HelperPE.Persistence.Extensions;
using HelperPE.Persistence.Repositories;

namespace HelperPE.Application.Services.Implementations
{
    public class TeacherServiceImpl : ITeacherService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITimeService _timeService;
        private readonly IPairRepository _pairRepository;
        private readonly DataContext _context;

        public TeacherServiceImpl(
            DataContext context,
            IUserRepository userRepository,
            ITimeService timeService,
            IPairRepository pairRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _timeService = timeService;
            _pairRepository = pairRepository;
        }

        public async Task<SubjectListModel> GetTeacherSubjects(Guid teacherId)
        {
            var teacher = await _userRepository.GetTeacherById(teacherId);

            var pairCounts = teacher.Pairs
                .GroupBy(p => p.Subject.Id)
                .ToDictionary(g => g.Key, g => g.Count());

            var subjectsWithCounts = teacher.Subjects
                .Select(s => new {
                    Subject = s,
                    Count = pairCounts.ContainsKey(s.Id) ? pairCounts[s.Id] : 0
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            return new SubjectListModel
            {
                Subjects = subjectsWithCounts.Select(x => x.Subject.ToDto()).ToList()
            };
        }

        public async Task<TeacherPairsModel> GetTodayPairs(Guid teacherId)
        {
            var teacher = await _userRepository.GetTeacherById(teacherId);

            var todayPairs = teacher.Pairs
                .Where(p => p.Date.Date == DateTime.Today)
                .ToList();

            return new TeacherPairsModel
            {
                Pairs = todayPairs.Select(p => p.ToDto()).ToList()
            };
        }

        public async Task CreatePair(Guid subjectId, Guid teacherId)
        {
            var teacher = await _userRepository.GetTeacherById(teacherId);
            var subject = await _pairRepository.GetSubject(subjectId);

            var currentPairNumber = _timeService.GetPairNumber();

            //if (currentPairNumber == -1)
            //    throw new BadRequestException(ErrorMessages.CAN_NOT_CREATE_PAIR);

            if (currentPairNumber == -1)
                currentPairNumber = 1;

            var newPair = new PairEntity
            {
                PairNumber = currentPairNumber,
                Subject = subject,
                Teacher = teacher,
            };

            teacher.Pairs.Add(newPair);
            _context.Pairs.Add(newPair);
            await _context.SaveChangesAsync();
        }
    }
}
