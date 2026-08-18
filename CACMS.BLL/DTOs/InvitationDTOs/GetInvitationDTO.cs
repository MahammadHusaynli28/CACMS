namespace CACMS.BLL.DTOs.InvitationDTOs;

public class GetInvitationDTO
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string? EventTitle { get; set; }
    public DateTime EventDate { get; set; }
    public string? LocationName { get; set; }
    public string PersonId { get; set; } = string.Empty;
    public string? PersonName { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
    public bool HasParticipation { get; set; }
    public string? SeatNumber { get; set; }
    public DateTime? CheckInTime { get; set; }
}
