using AutoMapper;
using CACMS.BLL.DTOs.EventDTOs;
using CACMS.BLL.Services.Interfaces;
using CACMS.DAL.Entities;
using CACMS.DAL.Repositories.Interfaces;

namespace CACMS.BLL.Services.Implementations;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public EventService(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ListEventDTO>> GetAllEventsAsync()
    {
        var events = await _eventRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ListEventDTO>>(events);
    }

    public async Task<GetEventDTO?> GetEventByIdAsync(int id)
    {
        var eventEntity = await _eventRepository.GetEventWithDetailsAsync(id);
        return eventEntity == null ? null : _mapper.Map<GetEventDTO>(eventEntity);
    }

    public async Task<IEnumerable<ListEventDTO>> GetUpcomingEventsAsync()
    {
        var events = await _eventRepository.GetUpcomingEventsAsync();
        return _mapper.Map<IEnumerable<ListEventDTO>>(events);
    }

    public async Task<IEnumerable<ListEventDTO>> GetTodayEventsAsync()
    {
        var events = await _eventRepository.GetTodayEventsAsync();
        return _mapper.Map<IEnumerable<ListEventDTO>>(events);
    }

    public async Task<IEnumerable<ListEventDTO>> GetEventsByOrganizerAsync(string organizerId)
    {
        var events = await _eventRepository.GetEventsByOrganizerAsync(organizerId);
        return _mapper.Map<IEnumerable<ListEventDTO>>(events);
    }

    public async Task<GetEventDTO> CreateEventAsync(CreateEventDTO createEventDTO, string organizerId)
    {
        var eventEntity = _mapper.Map<Event>(createEventDTO);
        eventEntity.OrganizerId = organizerId;

        var createdEvent = await _eventRepository.CreateAsync(eventEntity);
        await _eventRepository.SaveAsync();

        var result = await _eventRepository.GetEventWithDetailsAsync(createdEvent.Id);
        return _mapper.Map<GetEventDTO>(result!);
    }

    public async Task UpdateEventAsync(UpdateEventDTO updateEventDTO)
    {
        var eventEntity = _mapper.Map<Event>(updateEventDTO);
        _eventRepository.Update(eventEntity);
        await _eventRepository.SaveAsync();
    }

    public async Task DeleteEventAsync(int id)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(id);
        if (eventEntity != null)
        {
            _eventRepository.Delete(eventEntity);
            await _eventRepository.SaveAsync();
        }
    }
}
