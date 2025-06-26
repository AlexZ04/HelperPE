using HelperPE.Common.Enums;
using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Extensions
{
    public static class UserConverter
    {
        public static StudentProfileDTO ToDto(this StudentEntity model)
        {
            var classesAmount = 0;

            foreach (var attendance in model.EventsAttendances)
            {
                if (attendance.Status == EventApplicationStatus.Credited)
                    classesAmount += attendance.Event.ClassesAmount;
            }

            foreach (var attendance in model.OtherActivities)
                classesAmount += attendance.ClassesAmount;

            classesAmount += model.PairAttendances
                .Where(a => a.Status == PairAttendanceStatus.Accepted).Count();

            return new StudentProfileDTO
            {
                Id = model.Id,
                Email = model.Email,
                FullName = model.FullName,
                Role = model.Role,
                Course = model.Course,
                Group = model.Group,
                Faculty = model.Faculty.ToDto(),
                ClassesAmount = classesAmount
            };
        }

        public static SportsOrganizerProfileDTO ToDto(this SportsOrganizerEntity model)
        {
            var classesAmount = 0;

            foreach (var attendance in model.EventsAttendances)
            {
                if (attendance.Status == EventApplicationStatus.Credited)
                    classesAmount += attendance.Event.ClassesAmount;
            }

            foreach (var attendance in model.OtherActivities)
                classesAmount += attendance.ClassesAmount;

            classesAmount += model.PairAttendances
                .Where(a => a.Status == PairAttendanceStatus.Accepted).Count();

            return new SportsOrganizerProfileDTO
            {
                Id = model.Id,
                Email = model.Email,
                FullName = model.FullName,
                Role = model.Role,
                Course = model.Course,
                Group = model.Group,
                Faculty = model.Faculty.ToDto(),
                ClassesAmount = classesAmount,
                AppointmentDate = model.AppointmentDate
            };
        }

        public static TeacherProfileDTO ToDto(this TeacherEntity model)
        {
            return new TeacherProfileDTO
            {
                Id = model.Id,
                Email = model.Email,
                FullName = model.FullName,
                Role = model.Role,
                Subjects = model.Subjects
                                .Select(s => s.ToDto()).ToList()
            };
        }

        public static CuratorProfileDTO ToDto(this CuratorEntity model)
        {
            return new CuratorProfileDTO
            {
                Id = model.Id,
                Email = model.Email,
                FullName = model.FullName,
                Role = model.Role,
                Subjects = model.Subjects
                                .Select(s => s.ToDto()).ToList(),
                Faculties = model.Faculties
                                .Select(f => f.ToDto()).ToList()
            };
        }
    }
}
