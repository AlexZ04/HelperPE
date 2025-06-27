using HelperPE.Common.Models.Profile;

namespace HelperPE.Common.Models.Pairs
{
    public class PairShortModel
    {
        public Guid Id { get; set; }
        public int PairNumber { get; set; } = 1;
        public required TeacherProfileShortModel Teacher { get; set; }
        public required SubjectDTO Subject { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
