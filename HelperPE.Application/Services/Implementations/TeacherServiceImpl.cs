using HelperPE.Common.Constants;
using HelperPE.Common.Enums;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Pairs;
using HelperPE.Common.Models.Teacher;
using HelperPE.Infrastructure.Utilities;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Pairs;
using HelperPE.Persistence.Extensions;
using HelperPE.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Application.Services.Implementations
{
    public class TeacherServiceImpl : ITeacherService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPairRepository _pairRepository;
        private readonly DataContext _context;

        public TeacherServiceImpl(
            DataContext context,
            IUserRepository userRepository,
            IPairRepository pairRepository)
        {
            _context = context;
            _userRepository = userRepository;
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

            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            var todayPairs = teacher.Pairs
                .Where(p => p.Date >= today && p.Date < tomorrow)
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

            var currentPairNumber = TimeUtility.GetPairNumber();

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

        public async Task EditPairAttendanceStatus(
            Guid pairId, Guid userId,
            int classesAmount = 1, bool approve = true)
        {
            var attendance = await _context.PairsAttendances
                .Include(p => p.Student)
                .Include(p => p.Pair)
                .FirstOrDefaultAsync(a => a.StudentId == userId && a.PairId == pairId);

            if (attendance == null)
                throw new DirectoryNotFoundException(ErrorMessages.ATTENDANCE_NOT_FOUND);

            if (attendance.Status == PairAttendanceStatus.Accepted && approve ||
                !approve && attendance.Status == PairAttendanceStatus.Declined)
                throw new BadRequestException(ErrorMessages.ACTION_ALREADY_DONE);

            attendance.ClassesAmount = classesAmount;

            if (approve)
                attendance.Status = PairAttendanceStatus.Accepted;
            else
                attendance.Status = PairAttendanceStatus.Declined;

            await _context.SaveChangesAsync();
        }

        public async Task<PairAttendancesListModel> GetPairAttendances(Guid teacherId)
        {
            var teacher = await _userRepository.GetTeacherById(teacherId);

            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            var todayPairs = teacher.Pairs
                .Where(p => p.Date >= today && p.Date < tomorrow)
                .ToList();

            var pendingAttendances = todayPairs
                .SelectMany(p => p.Attendances)
                .Where(a => a.Status == PairAttendanceStatus.Pending)
                .Select(a => a.ToProfileDto())
                .ToList();

            return new PairAttendancesListModel
            {
                Attendances = pendingAttendances
            };
        }

        public async Task<PairAttendanceListShortModel> GetPendingPairAttendances(
            Guid pairId, Guid teacherId)
        {
            var teacher = await _userRepository.GetTeacherById(teacherId);

            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            var todayPairs = teacher.Pairs
                .Where(p => p.Date >= today && p.Date < tomorrow && p.PairId == pairId)
                .ToList();

            var pendingAttendances = todayPairs
                .SelectMany(p => p.Attendances)
                .Where(a => a.Status == PairAttendanceStatus.Pending)
                .Select(a => a.ToProfileDto())
                .ToList();

            return new PairAttendanceListShortModel
            {
                Attendances = pendingAttendances.Select(a =>
                    new PairAttendanceShortModel 
                    { 
                        Status = a.Status,
                        Student = a.Student,
                        ClassesAmount = a.ClassesAmount,
                    })
                    .ToList(),
            };
        }

        public async Task<PairAttendanceListShortModel> GetSolvedPairAttendances(
            Guid pairId, Guid teacherId)
        {
            var teacher = await _userRepository.GetTeacherById(teacherId);

            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            var todayPairs = teacher.Pairs
                .Where(p => p.Date >= today && p.Date < tomorrow && p.PairId == pairId)
                .ToList();

            var pendingAttendances = todayPairs
                .SelectMany(p => p.Attendances)
                .Where(a => a.Status != PairAttendanceStatus.Pending)
                .Select(a => a.ToProfileDto())
                .ToList();

            return new PairAttendanceListShortModel
            {
                Attendances = pendingAttendances.Select(a =>
                    new PairAttendanceShortModel
                    {
                        Status = a.Status,
                        Student = a.Student,
                        ClassesAmount = a.ClassesAmount,
                    })
                    .ToList(),
            };
        }

        public async Task<PairAttendanceListShortModel> GetAllPairAttendances(
            Guid pairId, Guid teacherId)
        {
            var teacher = await _userRepository.GetTeacherById(teacherId);

            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            var todayPairs = teacher.Pairs
                .Where(p => p.Date >= today && p.Date < tomorrow && p.PairId == pairId)
                .ToList();

            var pendingAttendances = todayPairs
                .SelectMany(p => p.Attendances)
                .Select(a => a.ToProfileDto())
                .OrderBy(a => a.Student.Name)
                .ToList();

            return new PairAttendanceListShortModel
            {
                Attendances = pendingAttendances.Select(a =>
                    new PairAttendanceShortModel
                    {
                        Status = a.Status,
                        Student = a.Student,
                        ClassesAmount = a.ClassesAmount,
                    })
                    .ToList(),
            };
        }
    }
}
