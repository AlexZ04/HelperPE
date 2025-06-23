using HelperPE.Common.Enums;
using HelperPE.Common.Models.Pairs;

namespace HelperPE.Common.Models.Profile
{
    public class CuratorProfileDTO
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public UserRole Role { get; set; }
        public List<SubjectDTO> Subjects { get; set; } = new List<SubjectDTO>();
        public List<FacultyDTO> Faculties { get; set; } = new List<FacultyDTO>();
    }
}
