using CACMS.DAL.Entities;

namespace CACMS.DAL.Repositories.Interfaces;

public interface IEventRepository : IGenericRepository<Event>
{
    Task<IEnumerable<Event>> GetEventsByOrganizerAsync(string organizerId);
    Task<IEnumerable<Event>> GetUpcomingEventsAsync();
    Task<IEnumerable<Event>> GetTodayEventsAsync();
    Task<Event?> GetEventWithDetailsAsync(int id);
}
