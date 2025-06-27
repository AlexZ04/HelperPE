using HelperPE.Common.Models.Attendances;
using HelperPE.Common.Models.Pairs;
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

        public static EventAttendanceFullModel ToFullDto(this EventAttendanceEntity model)
        {
            return new EventAttendanceFullModel
            {
                Event = model.Event.ToDto(),
                Profile = model.Student.ToDto(),
                Status = model.Status,
            };
        }

        public static PairAttendanceModel ToDto(this PairAttendanceEntity model)
        {
            return new PairAttendanceModel 
            {
                ClassesAmount = model.ClassesAmount,
                Pair = model.Pair.ToDto(),
                Status = model.Status
            };
        }

        public static PairAttendanceProfileModel ToProfileDto(this PairAttendanceEntity model)
        {
            return new PairAttendanceProfileModel
            {
                ClassesAmount = model.ClassesAmount,
                Status = model.Status,
                Student = model.Student.ToShortDto()
            };
        }

        public static OtherActivityModel ToDto(this OtherActivitiesEntity model)
        {
            return new OtherActivityModel
            {
                Id = model.Id,
                Comment = model.Comment,
                ClassesAmount = model.ClassesAmount,
                Date = model.Date,
            };
        }

        public static ProfileAttendanceEventModel ToProfileDto(this EventAttendanceEntity model)
        {
            return new ProfileAttendanceEventModel
            {
                Status = model.Status,
                Profile = model.Student.ToDto()
            };
        }
    }
}
