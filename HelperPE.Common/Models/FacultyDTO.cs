namespace HelperPE.Common.Models
{
    public class FacultyDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
    }
}
