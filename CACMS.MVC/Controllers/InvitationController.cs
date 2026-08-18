using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CACMS.BLL.DTOs.InvitationDTOs;
using CACMS.BLL.Services.Interfaces;
using System.Security.Claims;

namespace CACMS.MVC.Controllers;

[Authorize]
public class InvitationController : Controller
{
    private readonly IInvitationService _invitationService;
    private readonly IEventService _eventService;

    public InvitationController(IInvitationService invitationService, IEventService eventService)
    {
        _invitationService = invitationService;
        _eventService = eventService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var invitations = await _invitationService.GetInvitationsByPersonAsync(userId!);
        return View(invitations);
    }

    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> EventInvitations(int eventId)
    {
        var eventDto = await _eventService.GetEventByIdAsync(eventId);
        if (eventDto == null)
            return NotFound();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (eventDto.OrganizerId != userId)
            return Forbid();

        var invitations = await _invitationService.GetInvitationsByEventAsync(eventId);
        ViewData["EventTitle"] = eventDto.Title;
        ViewData["EventId"] = eventId;
        return View(invitations);
    }

    public async Task<IActionResult> Details(int id)
    {
        var invitation = await _invitationService.GetInvitationByIdAsync(id);
        if (invitation == null)
            return NotFound();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (invitation.PersonId != userId && !User.IsInRole("Organizer"))
            return Forbid();

        return View(invitation);
    }

    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> Create(int eventId)
    {
        var eventDto = await _eventService.GetEventByIdAsync(eventId);
        if (eventDto == null)
            return NotFound();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (eventDto.OrganizerId != userId)
            return Forbid();

        ViewData["EventId"] = eventId;
        ViewData["EventTitle"] = eventDto.Title;
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Organizer")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int eventId, CreateInvitationDTO createInvitationDTO)
    {
        var eventDto = await _eventService.GetEventByIdAsync(eventId);
        if (eventDto == null)
            return NotFound();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (eventDto.OrganizerId != userId)
            return Forbid();

        if (!ModelState.IsValid)
        {
            ViewData["EventId"] = eventId;
            ViewData["EventTitle"] = eventDto.Title;
            return View(createInvitationDTO);
        }

        try
        {
            createInvitationDTO.EventId = eventId;
            await _invitationService.CreateInvitationAsync(createInvitationDTO);
            TempData["Success"] = "Invitation sent successfully";
            return RedirectToAction(nameof(EventInvitations), new { eventId });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error: {ex.Message}");
            ViewData["EventId"] = eventId;
            ViewData["EventTitle"] = eventDto.Title;
            return View(createInvitationDTO);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Accept(int id)
    {
        try
        {
            var invitation = await _invitationService.GetInvitationByIdAsync(id);
            if (invitation == null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (invitation.PersonId != userId)
                return Forbid();

            await _invitationService.AcceptInvitationAsync(id);
            TempData["Success"] = "Invitation accepted";
            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error: {ex.Message}";
            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(int id)
    {
        try
        {
            var invitation = await _invitationService.GetInvitationByIdAsync(id);
            if (invitation == null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (invitation.PersonId != userId)
                return Forbid();

            await _invitationService.RejectInvitationAsync(id);
            TempData["Success"] = "Invitation rejected";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error: {ex.Message}";
            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckIn(int id)
    {
        try
        {
            var invitation = await _invitationService.GetInvitationByIdAsync(id);
            if (invitation == null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (invitation.PersonId != userId)
                return Forbid();

            await _invitationService.CheckInAsync(id);
            TempData["Success"] = "Checked in successfully";
            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error: {ex.Message}";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
