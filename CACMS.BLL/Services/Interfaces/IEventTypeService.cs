using CACMS.BLL.DTOs.EventTypeDTOs;

namespace CACMS.BLL.Services.Interfaces;

public interface IEventTypeService
{
    Task<IEnumerable<ListEventTypeDTO>> GetAllEventTypesAsync();
    Task<GetEventTypeDTO?> GetEventTypeByIdAsync(int id);
    Task<GetEventTypeDTO> CreateEventTypeAsync(CreateEventTypeDTO createEventTypeDTO);
    Task UpdateEventTypeAsync(UpdateEventTypeDTO updateEventTypeDTO);
    Task DeleteEventTypeAsync(int id);
}
