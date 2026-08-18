using System.ComponentModel.DataAnnotations;

namespace CACMS.BLL.DTOs.LocationDTOs;

public class UpdateLocationDTO
{
    [Required(ErrorMessage = "ID is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 150 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(300, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 300 characters")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Capacity is required")]
    [Range(1, 10000, ErrorMessage = "Capacity must be between 1 and 10000")]
    public int Capacity { get; set; }
}
