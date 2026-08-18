using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CACMS.BLL.DTOs.EventDTOs;
using CACMS.BLL.Services.Interfaces;
using System.Security.Claims;

namespace CACMS.MVC.Controllers;

[Authorize]
public class EventController : Controller
{
    private readonly IEventService _eventService;
    private readonly ILocationService _locationService;
    private readonly IEventTypeService _eventTypeService;

    public EventController(
        IEventService eventService,
        ILocationService locationService,
        IEventTypeService eventTypeService)
    {
        _eventService = eventService;
        _locationService = locationService;
        _eventTypeService = eventTypeService;
    }

    public async Task<IActionResult> Index()
    {
        var events = await _eventService.GetAllEventsAsync();
        return View(events);
    }

    public async Task<IActionResult> Upcoming()
    {
        var events = await _eventService.GetUpcomingEventsAsync();
        return View(events);
    }

    public async Task<IActionResult> Today()
    {
        var events = await _eventService.GetTodayEventsAsync();
        return View(events);
    }

    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> MyEvents()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var events = await _eventService.GetEventsByOrganizerAsync(userId!);
        return View(events);
    }

    public async Task<IActionResult> Details(int id)
    {
        var eventDto = await _eventService.GetEventByIdAsync(id);
        if (eventDto == null)
            return NotFound();

        return View(eventDto);
    }

    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> Create()
    {
        ViewData["Locations"] = await _locationService.GetAllLocationsAsync();
        ViewData["EventTypes"] = await _eventTypeService.GetAllEventTypesAsync();
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Organizer")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEventDTO createEventDTO)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Locations"] = await _locationService.GetAllLocationsAsync();
            ViewData["EventTypes"] = await _eventTypeService.GetAllEventTypesAsync();
            return View(createEventDTO);
        }

        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _eventService.CreateEventAsync(createEventDTO, userId!);
            TempData["Success"] = "Event created successfully";
            return RedirectToAction(nameof(MyEvents));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error: {ex.Message}");
            ViewData["Locations"] = await _locationService.GetAllLocationsAsync();
            ViewData["EventTypes"] = await _eventTypeService.GetAllEventTypesAsync();
            return View(createEventDTO);
        }
    }

    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> Edit(int id)
    {
        var eventDto = await _eventService.GetEventByIdAsync(id);
        if (eventDto == null)
            return NotFound();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (eventDto.OrganizerId != userId)
            return Forbid();

        var updateDto = new UpdateEventDTO
        {
            Id = eventDto.Id,
            Title = eventDto.Title,
            Description = eventDto.Description,
            Date = eventDto.Date,
            LocationId = eventDto.LocationId,
            EventTypeId = eventDto.EventTypeId,
            Capacity = eventDto.Capacity
        };

        ViewData["Locations"] = await _locationService.GetAllLocationsAsync();
        ViewData["EventTypes"] = await _eventTypeService.GetAllEventTypesAsync();
        return View(updateDto);
    }

    [HttpPost]
    [Authorize(Roles = "Organizer")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateEventDTO updateEventDTO)
    {
        if (id != updateEventDTO.Id)
            return NotFound();

        if (!ModelState.IsValid)
        {
            ViewData["Locations"] = await _locationService.GetAllLocationsAsync();
            ViewData["EventTypes"] = await _eventTypeService.GetAllEventTypesAsync();
            return View(updateEventDTO);
        }

        try
        {
            var eventDto = await _eventService.GetEventByIdAsync(id);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (eventDto?.OrganizerId != userId)
                return Forbid();

            await _eventService.UpdateEventAsync(updateEventDTO);
            TempData["Success"] = "Event updated successfully";
            return RedirectToAction(nameof(MyEvents));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error: {ex.Message}");
            ViewData["Locations"] = await _locationService.GetAllLocationsAsync();
            ViewData["EventTypes"] = await _eventTypeService.GetAllEventTypesAsync();
            return View(updateEventDTO);
        }
    }

    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> Delete(int id)
    {
        var eventDto = await _eventService.GetEventByIdAsync(id);
        if (eventDto == null)
            return NotFound();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (eventDto.OrganizerId != userId)
            return Forbid();

        return View(eventDto);
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Organizer")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var eventDto = await _eventService.GetEventByIdAsync(id);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (eventDto?.OrganizerId != userId)
                return Forbid();

            await _eventService.DeleteEventAsync(id);
            TempData["Success"] = "Event deleted successfully";
            return RedirectToAction(nameof(MyEvents));
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error: {ex.Message}";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
