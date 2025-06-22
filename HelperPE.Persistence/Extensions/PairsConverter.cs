using HelperPE.Common.Models.Pairs;
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
    }
}
