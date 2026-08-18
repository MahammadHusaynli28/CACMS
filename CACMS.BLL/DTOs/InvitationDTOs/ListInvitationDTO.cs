namespace CACMS.BLL.DTOs.InvitationDTOs;

public class ListInvitationDTO
{
    public int Id { get; set; }
    public string? EventTitle { get; set; }
    public DateTime EventDate { get; set; }
    public string? PersonName { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
}
