namespace CACMS.BLL.Services.Interfaces;

public interface IDashboardService
{
    Task<int> GetTotalEventsAsync();
    Task<int> GetTotalInvitationsAsync();
    Task<int> GetAcceptedInvitationsAsync();
    Task<int> GetRejectedInvitationsAsync();
    Task<int> GetTodayEventsAsync();
    Task<int> GetTotalParticipantsAsync();
}
