using System.ComponentModel.DataAnnotations;

namespace CACMS.BLL.DTOs.EventDTOs;

public class CreateEventDTO
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 1000 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Location is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid location")]
    public int LocationId { get; set; }

    [Required(ErrorMessage = "Event Type is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid event type")]
    public int EventTypeId { get; set; }

    [Required(ErrorMessage = "Capacity is required")]
    [Range(1, 10000, ErrorMessage = "Capacity must be between 1 and 10000")]
    public int Capacity { get; set; }
}
