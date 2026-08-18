using Microsoft.EntityFrameworkCore;
using CACMS.DAL.Data;
using CACMS.DAL.Entities;
using CACMS.DAL.Repositories.Interfaces;

namespace CACMS.DAL.Repositories.Implementations;

public class ParticipationRepository : GenericRepository<Participation>, IParticipationRepository
{
    private readonly ApplicationDbContext _context;

    public ParticipationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Participation?> GetByInvitationIdAsync(int invitationId)
    {
        return await _context.Participations
            .Include(p => p.Invitation)
                .ThenInclude(i => i!.Event)
            .FirstOrDefaultAsync(p => p.InvitationId == invitationId);
    }

    public async Task<IEnumerable<Participation>> GetParticipantsByEventAsync(int eventId)
    {
        return await _context.Participations
            .Include(p => p.Invitation)
                .ThenInclude(i => i!.Event)
            .Include(p => p.Invitation)
                .ThenInclude(i => i!.Person)
            .Where(p => p.Invitation!.EventId == eventId && p.CheckInTime.HasValue)
            .OrderBy(p => p.SeatNumber)
            .ToListAsync();
    }

    public async Task<int> GetTotalParticipantsAsync()
    {
        return await _context.Participations
            .Where(p => p.CheckInTime.HasValue)
            .CountAsync();
    }

    public async Task<string> GetNextSeatNumberAsync(int eventId)
    {
        var lastParticipation = await _context.Participations
            .Include(p => p.Invitation)
            .Where(p => p.Invitation!.EventId == eventId)
            .OrderByDescending(p => p.SeatNumber)
            .FirstOrDefaultAsync();

        if (lastParticipation == null)
        {
            return "A001";
        }

        var lastSeat = lastParticipation.SeatNumber;
        if (string.IsNullOrEmpty(lastSeat) || lastSeat.Length < 4)
        {
            return "A001";
        }

        var section = lastSeat[0];
        if (int.TryParse(lastSeat.Substring(1), out var number))
        {
            number++;
            if (number > 999)
            {
                section = (char)(section + 1);
                number = 1;
            }
            return $"{section}{number:D3}";
        }

        return "A001";
    }
}
