namespace CACMS.BLL.DTOs.LocationDTOs;

public class GetLocationDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int EventCount { get; set; }
}
