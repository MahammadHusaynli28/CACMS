using CACMS.BLL.DTOs.InvitationDTOs;

namespace CACMS.BLL.Services.Interfaces;

public interface IInvitationService
{
    Task<IEnumerable<ListInvitationDTO>> GetAllInvitationsAsync();
    Task<GetInvitationDTO?> GetInvitationByIdAsync(int id);
    Task<IEnumerable<ListInvitationDTO>> GetInvitationsByPersonAsync(string personId);
    Task<IEnumerable<ListInvitationDTO>> GetInvitationsByEventAsync(int eventId);
    Task<GetInvitationDTO> CreateInvitationAsync(CreateInvitationDTO createInvitationDTO);
    Task AcceptInvitationAsync(int invitationId);
    Task RejectInvitationAsync(int invitationId);
    Task CheckInAsync(int invitationId);
    Task<int> GetTotalInvitationsCountAsync();
    Task<int> GetAcceptedInvitationsCountAsync();
    Task<int> GetRejectedInvitationsCountAsync();
}
