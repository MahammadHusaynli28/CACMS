using CACMS.DAL.Entities;

namespace CACMS.DAL.Repositories.Interfaces;

public interface ILocationRepository : IGenericRepository<Location>
{
    Task<Location?> GetLocationWithEventsAsync(int id);
}
