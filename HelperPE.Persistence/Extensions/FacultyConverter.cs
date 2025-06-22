using HelperPE.Common.Models;
using HelperPE.Common.Models.Profile;
using HelperPE.Persistence.Entities.Faculty;
using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Extensions
{
    public static class FacultyConverter
    {
        public static FacultyDTO ToDto(this FacultyEntity model)
        {
            return new FacultyDTO
            {
                Id = model.Id,
                Name = model.Name,
            };
        }
    }
}
