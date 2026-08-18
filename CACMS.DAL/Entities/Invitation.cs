using CACMS.DAL.Entities.Enums;

namespace CACMS.DAL.Entities;

public class Invitation
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string PersonId { get; set; } = string.Empty;
    public InvitationStatus Status { get; set; }
    public DateTime SentAt { get; set; }

    // Navigation properties
    public Event? Event { get; set; }
    public ApplicationUser? Person { get; set; }
    public Participation? Participation { get; set; }
}
