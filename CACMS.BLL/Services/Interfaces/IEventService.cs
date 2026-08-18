using CACMS.BLL.DTOs.EventDTOs;

namespace CACMS.BLL.Services.Interfaces;

public interface IEventService
{
    Task<IEnumerable<ListEventDTO>> GetAllEventsAsync();
    Task<GetEventDTO?> GetEventByIdAsync(int id);
    Task<IEnumerable<ListEventDTO>> GetUpcomingEventsAsync();
    Task<IEnumerable<ListEventDTO>> GetTodayEventsAsync();
    Task<IEnumerable<ListEventDTO>> GetEventsByOrganizerAsync(string organizerId);
    Task<GetEventDTO> CreateEventAsync(CreateEventDTO createEventDTO, string organizerId);
    Task UpdateEventAsync(UpdateEventDTO updateEventDTO);
    Task DeleteEventAsync(int id);
}
