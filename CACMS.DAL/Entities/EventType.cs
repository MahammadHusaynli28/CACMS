namespace CACMS.DAL.Entities;

public class EventType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
