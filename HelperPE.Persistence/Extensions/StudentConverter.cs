using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Extensions
{
    public static class StudentConverter
    {
        public static StudentProfileDTO ToDto(this StudentEntity model)
        {
            var classesAmount = 0;

            foreach (var attendance in model.EventsAttendances)
                classesAmount += attendance.Event.ClassesAmount;

            foreach (var attendance in model.OtherActivities)
                classesAmount += attendance.ClassesAmount;

            classesAmount += model.PairAttendances.Count();

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
    }
}
