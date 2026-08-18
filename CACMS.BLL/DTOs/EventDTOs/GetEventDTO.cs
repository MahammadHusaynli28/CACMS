namespace CACMS.BLL.DTOs.EventDTOs;

public class GetEventDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int LocationId { get; set; }
    public string? LocationName { get; set; }
    public int EventTypeId { get; set; }
    public string? EventTypeName { get; set; }
    public string OrganizerId { get; set; } = string.Empty;
    public string? OrganizerName { get; set; }
    public int Capacity { get; set; }
    public DateTime CreatedDate { get; set; }
    public int InvitationCount { get; set; }
    public int AcceptedCount { get; set; }
    public int ParticipantCount { get; set; }
}
