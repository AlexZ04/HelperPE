using HelperPE.Common.Enums;

namespace HelperPE.Common.Models.Curator
{
    public class StudentProfileShortModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Course { get; set; }
        public required string Group { get; set; }
        public UserRole Role { get; set; }
    }
}
