using HelperPE.Common.Models.Profile;

namespace HelperPE.Common.Models.Attendances
{
    public class OtherActivityModel
    {
        public Guid Id { get; set; }
        public required string Comment { get; set; }
        public int ClassesAmount { get; set; } = 1;
        public DateTime Date { get; set; }
    }
}
