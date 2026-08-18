using CACMS.DAL.Entities;

namespace CACMS.DAL.Repositories.Interfaces;

public interface IParticipationRepository : IGenericRepository<Participation>
{
    Task<Participation?> GetByInvitationIdAsync(int invitationId);
    Task<IEnumerable<Participation>> GetParticipantsByEventAsync(int eventId);
    Task<int> GetTotalParticipantsAsync();
    Task<string> GetNextSeatNumberAsync(int eventId);
}
