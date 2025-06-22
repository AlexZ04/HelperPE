using HelperPE.Common.Enums;
using HelperPE.Persistence.Entities.Faculty;

namespace HelperPE.Common.Models.Profile
{
    public class StudentProfileDTO
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public UserRole Role { get; set; }
        public required int Course { get; set; }
        public required string Group { get; set; }
        public required FacultyEntity Faculty { get; set; }
        public required int ClassesAmount { get; set; }
    }
}
