namespace CACMS.DAL.Entities;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int LocationId { get; set; }
    public int EventTypeId { get; set; }
    public string OrganizerId { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public DateTime CreatedDate { get; set; }

    // Navigation properties
    public Location? Location { get; set; }
    public EventType? EventType { get; set; }
    public ApplicationUser? Organizer { get; set; }
    public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
}
