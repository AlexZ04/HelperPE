namespace HelperPE.Persistence.Entities.Faculty
{
    public class FacultyEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }   
    }
}
