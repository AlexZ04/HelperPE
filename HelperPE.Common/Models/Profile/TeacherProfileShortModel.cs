using HelperPE.Common.Enums;

namespace HelperPE.Common.Models.Profile
{
    public class TeacherProfileShortModel
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public UserRole Role { get; set; }
    }
}
