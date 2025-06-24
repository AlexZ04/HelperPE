namespace HelperPE.Common.Models.Event
{
    public class EventModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int ClassesAmount { get; set; } = 1;
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
