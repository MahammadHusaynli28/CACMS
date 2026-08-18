namespace CACMS.DAL.Entities;

public class Participation
{
    public int Id { get; set; }
    public int InvitationId { get; set; }
    public DateTime? CheckInTime { get; set; }
    public string SeatNumber { get; set; } = string.Empty;

    // Navigation properties
    public Invitation? Invitation { get; set; }
}
