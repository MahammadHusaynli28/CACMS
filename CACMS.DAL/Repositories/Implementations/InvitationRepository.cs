using Microsoft.EntityFrameworkCore;
using CACMS.DAL.Data;
using CACMS.DAL.Entities;
using CACMS.DAL.Entities.Enums;
using CACMS.DAL.Repositories.Interfaces;

namespace CACMS.DAL.Repositories.Implementations;

public class InvitationRepository : GenericRepository<Invitation>, IInvitationRepository
{
    private readonly ApplicationDbContext _context;

    public InvitationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Invitation>> GetInvitationsByPersonAsync(string personId)
    {
        return await _context.Invitations
            .Where(i => i.PersonId == personId)
            .Include(i => i.Event)
                .ThenInclude(e => e!.Location)
            .Include(i => i.Event)
                .ThenInclude(e => e!.EventType)
            .Include(i => i.Participation)
            .OrderByDescending(i => i.SentAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invitation>> GetInvitationsByEventAsync(int eventId)
    {
        return await _context.Invitations
            .Where(i => i.EventId == eventId)
            .Include(i => i.Person)
            .Include(i => i.Participation)
            .OrderBy(i => i.SentAt)
            .ToListAsync();
    }

    public async Task<Invitation?> GetInvitationWithDetailsAsync(int id)
    {
        return await _context.Invitations
            .Include(i => i.Event)
                .ThenInclude(e => e!.Location)
            .Include(i => i.Event)
                .ThenInclude(e => e!.EventType)
            .Include(i => i.Person)
            .Include(i => i.Participation)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<int> GetAcceptedInvitationsCountAsync()
    {
        return await _context.Invitations
            .Where(i => i.Status == InvitationStatus.Accepted)
            .CountAsync();
    }

    public async Task<int> GetRejectedInvitationsCountAsync()
    {
        return await _context.Invitations
            .Where(i => i.Status == InvitationStatus.Rejected)
            .CountAsync();
    }

    public async Task<bool> HasPendingInvitationAsync(int eventId, string personId)
    {
        return await _context.Invitations
            .AnyAsync(i => i.EventId == eventId && i.PersonId == personId && i.Status == InvitationStatus.Pending);
    }
}
