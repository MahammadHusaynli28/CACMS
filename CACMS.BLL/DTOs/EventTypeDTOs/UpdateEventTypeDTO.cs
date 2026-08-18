using System.ComponentModel.DataAnnotations;

namespace CACMS.BLL.DTOs.EventTypeDTOs;

public class UpdateEventTypeDTO
{
    [Required(ErrorMessage = "ID is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name { get; set; } = string.Empty;
}
