namespace CACMS.BLL.DTOs.EventDTOs;

public class ListEventDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? LocationName { get; set; }
    public string? EventTypeName { get; set; }
    public int Capacity { get; set; }
    public int InvitationCount { get; set; }
}
