using CACMS.DAL.Entities;

namespace CACMS.DAL.Repositories.Interfaces;

public interface IEventTypeRepository : IGenericRepository<EventType>
{
    Task<EventType?> GetEventTypeWithEventsAsync(int id);
}
