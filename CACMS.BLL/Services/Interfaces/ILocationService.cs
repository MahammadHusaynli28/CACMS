using CACMS.BLL.DTOs.LocationDTOs;

namespace CACMS.BLL.Services.Interfaces;

public interface ILocationService
{
    Task<IEnumerable<ListLocationDTO>> GetAllLocationsAsync();
    Task<GetLocationDTO?> GetLocationByIdAsync(int id);
    Task<GetLocationDTO> CreateLocationAsync(CreateLocationDTO createLocationDTO);
    Task UpdateLocationAsync(UpdateLocationDTO updateLocationDTO);
    Task DeleteLocationAsync(int id);
}
