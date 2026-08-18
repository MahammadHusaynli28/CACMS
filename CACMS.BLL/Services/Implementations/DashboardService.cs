using CACMS.BLL.Services.Interfaces;
using CACMS.DAL.Repositories.Interfaces;

namespace CACMS.BLL.Services.Implementations;

public class DashboardService : IDashboardService
{
    private readonly IEventRepository _eventRepository;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IParticipationRepository _participationRepository;

    public DashboardService(
        IEventRepository eventRepository,
        IInvitationRepository invitationRepository,
        IParticipationRepository participationRepository)
    {
        _eventRepository = eventRepository;
        _invitationRepository = invitationRepository;
        _participationRepository = participationRepository;
    }

    public async Task<int> GetTotalEventsAsync()
    {
        var events = await _eventRepository.GetAllAsync();
        return events.Count();
    }

    public async Task<int> GetTotalInvitationsAsync()
    {
        var invitations = await _invitationRepository.GetAllAsync();
        return invitations.Count();
    }

    public async Task<int> GetAcceptedInvitationsAsync()
    {
        return await _invitationRepository.GetAcceptedInvitationsCountAsync();
    }

    public async Task<int> GetRejectedInvitationsAsync()
    {
        return await _invitationRepository.GetRejectedInvitationsCountAsync();
    }

    public async Task<int> GetTodayEventsAsync()
    {
        var events = await _eventRepository.GetTodayEventsAsync();
        return events.Count();
    }

    public async Task<int> GetTotalParticipantsAsync()
    {
        return await _participationRepository.GetTotalParticipantsAsync();
    }
}
