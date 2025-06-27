using HelperPE.Common.Models.Curator;
using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Pairs;
using HelperPE.Persistence.Entities.Events;
using HelperPE.Persistence.Entities.Faculty;
using HelperPE.Persistence.Entities.Pairs;
using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Extensions
{
    public static class ActivitiesConverter
    {
        public static SubjectDTO ToDto(this SubjectEntity model)
        {
            return new SubjectDTO
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        public static EventModel ToDto(this EventEntity model)
        {
            return new EventModel
            {
                Id = model.EventId,
                Name = model.Name,
                ClassesAmount = model.ClassesAmount,
                Description = model.Description,
                Date = model.Date,
                Faculty = model.Faculty.ToDto(),
            };
        }

        public static EventFullModel ToFullDto(this EventEntity model)
        {
            return new EventFullModel
            {
                Id = model.EventId,
                Name = model.Name,
                ClassesAmount = model.ClassesAmount,
                Description = model.Description,
                Date = model.Date,
                Faculty = model.Faculty.ToDto(),
                Attendances = model.Attendances.Select(a => a.ToProfileDto()).ToList()
            };
        }

        public static PairModel ToDto(this PairEntity model)
        {
            return new PairModel
            {
                Id = model.PairId,
                PairNumber = model.PairNumber,
                Teacher = model.Teacher.ToDto(),
                Subject = model.Subject.ToDto(),
            };
        }

        public static EventEntity CreateEvent(this EventCreateModel model, FacultyEntity faculty)
        {
            return new EventEntity
            {
                Faculty = faculty,
                Name = model.Name,
                Description = model.Description,
                Date = model.Date,
                ClassesAmount = model.ClassesAmount
            };
        }

        public static OtherActivitiesEntity CreateOtherActivity(
            this OtherActivityCreateModel model, CuratorEntity curator, StudentEntity student)
        {
            return new OtherActivitiesEntity
            {
                Teacher = curator,
                Student = student,
                Comment = model.Comment,
                ClassesAmount = model.ClassesAmount,
            };
        }
    }
}
