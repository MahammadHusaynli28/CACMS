using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CACMS.BLL.Services.Interfaces;

namespace CACMS.MVC.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var totalEvents = await _dashboardService.GetTotalEventsAsync();
        var totalInvitations = await _dashboardService.GetTotalInvitationsAsync();
        var acceptedInvitations = await _dashboardService.GetAcceptedInvitationsAsync();
        var rejectedInvitations = await _dashboardService.GetRejectedInvitationsAsync();
        var todayEvents = await _dashboardService.GetTodayEventsAsync();
        var totalParticipants = await _dashboardService.GetTotalParticipantsAsync();

        ViewData["TotalEvents"] = totalEvents;
        ViewData["TotalInvitations"] = totalInvitations;
        ViewData["AcceptedInvitations"] = acceptedInvitations;
        ViewData["RejectedInvitations"] = rejectedInvitations;
        ViewData["TodayEvents"] = todayEvents;
        ViewData["TotalParticipants"] = totalParticipants;

        return View();
    }
}
