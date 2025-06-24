using HelperPE.Common.Models.Attendances;
using HelperPE.Persistence.Entities.Events;
using HelperPE.Persistence.Entities.Pairs;

namespace HelperPE.Persistence.Extensions
{
    public static class AttendancesConverter
    {
        public static EventAttendanceModel ToDto(this EventAttendanceEntity model)
        {
            return new EventAttendanceModel
            {
                Event = model.Event.ToDto(),
                Status = model.Status,
            };
        }

        public static PairAttendanceModel ToDto(this PairAttendanceEntity model)
        {
            return new PairAttendanceModel 
            {
                ClassesAmount = model.ClassesAmount,
                Pair = model.Pair.ToDto(),
            };
        }

        public static OtherActivityModel ToDto(this OtherActivitiesEntity model)
        {
            return new OtherActivityModel
            {
                Id = model.Id,
                Comment = model.Comment,
                ClassesAmount = model.ClassesAmount,
                Student = model.Student.ToDto(),
                Teacher = model.Teacher.ToDto(),
                Date = model.Date,
            };
        }
    }
}
