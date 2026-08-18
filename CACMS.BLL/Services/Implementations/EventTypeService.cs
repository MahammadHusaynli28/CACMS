using AutoMapper;
using CACMS.BLL.DTOs.EventTypeDTOs;
using CACMS.BLL.Services.Interfaces;
using CACMS.DAL.Entities;
using CACMS.DAL.Repositories.Interfaces;

namespace CACMS.BLL.Services.Implementations;

public class EventTypeService : IEventTypeService
{
    private readonly IEventTypeRepository _eventTypeRepository;
    private readonly IMapper _mapper;

    public EventTypeService(IEventTypeRepository eventTypeRepository, IMapper mapper)
    {
        _eventTypeRepository = eventTypeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ListEventTypeDTO>> GetAllEventTypesAsync()
    {
        var eventTypes = await _eventTypeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ListEventTypeDTO>>(eventTypes);
    }

    public async Task<GetEventTypeDTO?> GetEventTypeByIdAsync(int id)
    {
        var eventType = await _eventTypeRepository.GetEventTypeWithEventsAsync(id);
        return eventType == null ? null : _mapper.Map<GetEventTypeDTO>(eventType);
    }

    public async Task<GetEventTypeDTO> CreateEventTypeAsync(CreateEventTypeDTO createEventTypeDTO)
    {
        var eventType = _mapper.Map<EventType>(createEventTypeDTO);
        var createdEventType = await _eventTypeRepository.CreateAsync(eventType);
        await _eventTypeRepository.SaveAsync();

        var result = await _eventTypeRepository.GetEventTypeWithEventsAsync(createdEventType.Id);
        return _mapper.Map<GetEventTypeDTO>(result!);
    }

    public async Task UpdateEventTypeAsync(UpdateEventTypeDTO updateEventTypeDTO)
    {
        var eventType = _mapper.Map<EventType>(updateEventTypeDTO);
        _eventTypeRepository.Update(eventType);
        await _eventTypeRepository.SaveAsync();
    }

    public async Task DeleteEventTypeAsync(int id)
    {
        var eventType = await _eventTypeRepository.GetByIdAsync(id);
        if (eventType != null)
        {
            _eventTypeRepository.Delete(eventType);
            await _eventTypeRepository.SaveAsync();
        }
    }
}
