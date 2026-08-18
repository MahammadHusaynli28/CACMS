using Microsoft.EntityFrameworkCore;
using CACMS.DAL.Data;
using CACMS.DAL.Entities;
using CACMS.DAL.Repositories.Interfaces;

namespace CACMS.DAL.Repositories.Implementations;

public class LocationRepository : GenericRepository<Location>, ILocationRepository
{
    private readonly ApplicationDbContext _context;

    public LocationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Location?> GetLocationWithEventsAsync(int id)
    {
        return await _context.Locations
            .Include(l => l.Events)
            .FirstOrDefaultAsync(l => l.Id == id);
    }
}
