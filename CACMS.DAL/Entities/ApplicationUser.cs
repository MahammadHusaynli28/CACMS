using Microsoft.AspNetCore.Identity;
using CACMS.DAL.Entities.Enums;

namespace CACMS.DAL.Entities;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public UserRole Role { get; set; }

    // Navigation properties
    public ICollection<Event> OrganizedEvents { get; set; } = new List<Event>();
    public ICollection<Invitation> ReceivedInvitations { get; set; } = new List<Invitation>();
}
