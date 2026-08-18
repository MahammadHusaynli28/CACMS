using CACMS.DAL.Entities;
using CACMS.DAL.Entities.Enums;

namespace CACMS.DAL.Repositories.Interfaces;

public interface IInvitationRepository : IGenericRepository<Invitation>
{
    Task<IEnumerable<Invitation>> GetInvitationsByPersonAsync(string personId);
    Task<IEnumerable<Invitation>> GetInvitationsByEventAsync(int eventId);
    Task<Invitation?> GetInvitationWithDetailsAsync(int id);
    Task<int> GetAcceptedInvitationsCountAsync();
    Task<int> GetRejectedInvitationsCountAsync();
    Task<bool> HasPendingInvitationAsync(int eventId, string personId);
}
