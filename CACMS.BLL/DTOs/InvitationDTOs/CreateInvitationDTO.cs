using System.ComponentModel.DataAnnotations;

namespace CACMS.BLL.DTOs.InvitationDTOs;

public class CreateInvitationDTO
{
    [Required(ErrorMessage = "Event is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid event")]
    public int EventId { get; set; }

    [Required(ErrorMessage = "Person is required")]
    public string PersonId { get; set; } = string.Empty;
}
