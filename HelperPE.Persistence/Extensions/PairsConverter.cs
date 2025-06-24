using HelperPE.Common.Models.Event;
using HelperPE.Common.Models.Pairs;
using HelperPE.Persistence.Entities.Events;
using HelperPE.Persistence.Entities.Pairs;

namespace HelperPE.Persistence.Extensions
{
    public static class PairsConverter
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
    }
}
