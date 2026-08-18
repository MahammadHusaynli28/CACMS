namespace CACMS.BLL.DTOs.EventTypeDTOs;

public class GetEventTypeDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int EventCount { get; set; }
}
