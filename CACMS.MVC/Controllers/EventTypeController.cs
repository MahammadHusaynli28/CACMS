using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CACMS.BLL.DTOs.EventTypeDTOs;
using CACMS.BLL.Services.Interfaces;

namespace CACMS.MVC.Controllers;

[Authorize(Roles = "Admin")]
public class EventTypeController : Controller
{
    private readonly IEventTypeService _eventTypeService;

    public EventTypeController(IEventTypeService eventTypeService)
    {
        _eventTypeService = eventTypeService;
    }

    public async Task<IActionResult> Index()
    {
        var eventTypes = await _eventTypeService.GetAllEventTypesAsync();
        return View(eventTypes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var eventType = await _eventTypeService.GetEventTypeByIdAsync(id);
        if (eventType == null)
            return NotFound();

        return View(eventType);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEventTypeDTO createEventTypeDTO)
    {
        if (!ModelState.IsValid)
            return View(createEventTypeDTO);

        try
        {
            await _eventTypeService.CreateEventTypeAsync(createEventTypeDTO);
            TempData["Success"] = "Event Type created successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error: {ex.Message}");
            return View(createEventTypeDTO);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var eventType = await _eventTypeService.GetEventTypeByIdAsync(id);
        if (eventType == null)
            return NotFound();

        var updateDto = new UpdateEventTypeDTO
        {
            Id = eventType.Id,
            Name = eventType.Name
        };

        return View(updateDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateEventTypeDTO updateEventTypeDTO)
    {
        if (id != updateEventTypeDTO.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(updateEventTypeDTO);

        try
        {
            await _eventTypeService.UpdateEventTypeAsync(updateEventTypeDTO);
            TempData["Success"] = "Event Type updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error: {ex.Message}");
            return View(updateEventTypeDTO);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var eventType = await _eventTypeService.GetEventTypeByIdAsync(id);
        if (eventType == null)
            return NotFound();

        return View(eventType);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _eventTypeService.DeleteEventTypeAsync(id);
            TempData["Success"] = "Event Type deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error: {ex.Message}";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
