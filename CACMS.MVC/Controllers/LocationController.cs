using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CACMS.BLL.DTOs.LocationDTOs;
using CACMS.BLL.Services.Interfaces;

namespace CACMS.MVC.Controllers;

[Authorize(Roles = "Admin")]
public class LocationController : Controller
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public async Task<IActionResult> Index()
    {
        var locations = await _locationService.GetAllLocationsAsync();
        return View(locations);
    }

    public async Task<IActionResult> Details(int id)
    {
        var location = await _locationService.GetLocationByIdAsync(id);
        if (location == null)
            return NotFound();

        return View(location);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateLocationDTO createLocationDTO)
    {
        if (!ModelState.IsValid)
            return View(createLocationDTO);

        try
        {
            await _locationService.CreateLocationAsync(createLocationDTO);
            TempData["Success"] = "Location created successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error: {ex.Message}");
            return View(createLocationDTO);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var location = await _locationService.GetLocationByIdAsync(id);
        if (location == null)
            return NotFound();

        var updateDto = new UpdateLocationDTO
        {
            Id = location.Id,
            Name = location.Name,
            Address = location.Address,
            Capacity = location.Capacity
        };

        return View(updateDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateLocationDTO updateLocationDTO)
    {
        if (id != updateLocationDTO.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(updateLocationDTO);

        try
        {
            await _locationService.UpdateLocationAsync(updateLocationDTO);
            TempData["Success"] = "Location updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error: {ex.Message}");
            return View(updateLocationDTO);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var location = await _locationService.GetLocationByIdAsync(id);
        if (location == null)
            return NotFound();

        return View(location);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _locationService.DeleteLocationAsync(id);
            TempData["Success"] = "Location deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error: {ex.Message}";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
