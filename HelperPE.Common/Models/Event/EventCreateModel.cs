using System.ComponentModel.DataAnnotations;

namespace HelperPE.Common.Models.Event
{
    public class EventCreateModel
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public int ClassesAmount { get; set; } = 1;
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
