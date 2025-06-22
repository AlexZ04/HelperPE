namespace HelperPE.Common.Models.Pairs
{
    public class SubjectDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
    }
}
