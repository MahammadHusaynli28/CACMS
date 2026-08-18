using AutoMapper;
using CACMS.BLL.DTOs.LocationDTOs;
using CACMS.BLL.Services.Interfaces;
using CACMS.DAL.Entities;
using CACMS.DAL.Repositories.Interfaces;

namespace CACMS.BLL.Services.Implementations;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public LocationService(ILocationRepository locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ListLocationDTO>> GetAllLocationsAsync()
    {
        var locations = await _locationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ListLocationDTO>>(locations);
    }

    public async Task<GetLocationDTO?> GetLocationByIdAsync(int id)
    {
        var location = await _locationRepository.GetLocationWithEventsAsync(id);
        return location == null ? null : _mapper.Map<GetLocationDTO>(location);
    }

    public async Task<GetLocationDTO> CreateLocationAsync(CreateLocationDTO createLocationDTO)
    {
        var location = _mapper.Map<Location>(createLocationDTO);
        var createdLocation = await _locationRepository.CreateAsync(location);
        await _locationRepository.SaveAsync();

        var result = await _locationRepository.GetLocationWithEventsAsync(createdLocation.Id);
        return _mapper.Map<GetLocationDTO>(result!);
    }

    public async Task UpdateLocationAsync(UpdateLocationDTO updateLocationDTO)
    {
        var location = _mapper.Map<Location>(updateLocationDTO);
        _locationRepository.Update(location);
        await _locationRepository.SaveAsync();
    }

    public async Task DeleteLocationAsync(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location != null)
        {
            _locationRepository.Delete(location);
            await _locationRepository.SaveAsync();
        }
    }
}
