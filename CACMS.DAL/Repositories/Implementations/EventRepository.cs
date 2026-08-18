using Microsoft.EntityFrameworkCore;
using CACMS.DAL.Data;
using CACMS.DAL.Entities;
using CACMS.DAL.Repositories.Interfaces;

namespace CACMS.DAL.Repositories.Implementations;

public class EventRepository : GenericRepository<Event>, IEventRepository
{
    private readonly ApplicationDbContext _context;

    public EventRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetEventsByOrganizerAsync(string organizerId)
    {
        return await _context.Events
            .Where(e => e.OrganizerId == organizerId)
            .Include(e => e.Location)
            .Include(e => e.EventType)
            .Include(e => e.Organizer)
            .OrderByDescending(e => e.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetUpcomingEventsAsync()
    {
        var now = DateTime.UtcNow;
        return await _context.Events
            .Where(e => e.Date > now)
            .Include(e => e.Location)
            .Include(e => e.EventType)
            .Include(e => e.Organizer)
            .OrderBy(e => e.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetTodayEventsAsync()
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);

        return await _context.Events
            .Where(e => e.Date >= today && e.Date < tomorrow)
            .Include(e => e.Location)
            .Include(e => e.EventType)
            .Include(e => e.Organizer)
            .OrderBy(e => e.Date)
            .ToListAsync();
    }

    public async Task<Event?> GetEventWithDetailsAsync(int id)
    {
        return await _context.Events
            .Include(e => e.Location)
            .Include(e => e.EventType)
            .Include(e => e.Organizer)
            .Include(e => e.Invitations)
                .ThenInclude(i => i.Person)
            .Include(e => e.Invitations)
                .ThenInclude(i => i.Participation)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}
