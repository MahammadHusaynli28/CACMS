using Microsoft.EntityFrameworkCore;
using CACMS.DAL.Data;
using CACMS.DAL.Entities;
using CACMS.DAL.Repositories.Interfaces;

namespace CACMS.DAL.Repositories.Implementations;

public class EventTypeRepository : GenericRepository<EventType>, IEventTypeRepository
{
    private readonly ApplicationDbContext _context;

    public EventTypeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<EventType?> GetEventTypeWithEventsAsync(int id)
    {
        return await _context.EventTypes
            .Include(et => et.Events)
            .FirstOrDefaultAsync(et => et.Id == id);
    }
}
