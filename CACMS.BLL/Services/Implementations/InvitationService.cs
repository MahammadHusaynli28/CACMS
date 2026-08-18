using AutoMapper;
using CACMS.BLL.DTOs.InvitationDTOs;
using CACMS.BLL.Services.Interfaces;
using CACMS.DAL.Entities;
using CACMS.DAL.Entities.Enums;
using CACMS.DAL.Repositories.Interfaces;

namespace CACMS.BLL.Services.Implementations;

public class InvitationService : IInvitationService
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IParticipationRepository _participationRepository;
    private readonly IMapper _mapper;

    public InvitationService(
        IInvitationRepository invitationRepository,
        IParticipationRepository participationRepository,
        IMapper mapper)
    {
        _invitationRepository = invitationRepository;
        _participationRepository = participationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ListInvitationDTO>> GetAllInvitationsAsync()
    {
        var invitations = await _invitationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ListInvitationDTO>>(invitations);
    }

    public async Task<GetInvitationDTO?> GetInvitationByIdAsync(int id)
    {
        var invitation = await _invitationRepository.GetInvitationWithDetailsAsync(id);
        return invitation == null ? null : _mapper.Map<GetInvitationDTO>(invitation);
    }

    public async Task<IEnumerable<ListInvitationDTO>> GetInvitationsByPersonAsync(string personId)
    {
        var invitations = await _invitationRepository.GetInvitationsByPersonAsync(personId);
        return _mapper.Map<IEnumerable<ListInvitationDTO>>(invitations);
    }

    public async Task<IEnumerable<ListInvitationDTO>> GetInvitationsByEventAsync(int eventId)
    {
        var invitations = await _invitationRepository.GetInvitationsByEventAsync(eventId);
        return _mapper.Map<IEnumerable<ListInvitationDTO>>(invitations);
    }

    public async Task<GetInvitationDTO> CreateInvitationAsync(CreateInvitationDTO createInvitationDTO)
    {
        var invitation = _mapper.Map<Invitation>(createInvitationDTO);
        var createdInvitation = await _invitationRepository.CreateAsync(invitation);
        await _invitationRepository.SaveAsync();

        var result = await _invitationRepository.GetInvitationWithDetailsAsync(createdInvitation.Id);
        return _mapper.Map<GetInvitationDTO>(result!);
    }

    public async Task AcceptInvitationAsync(int invitationId)
    {
        var invitation = await _invitationRepository.GetByIdAsync(invitationId);
        if (invitation == null)
            throw new InvalidOperationException("Invitation not found");

        if (invitation.Status == InvitationStatus.Accepted)
            throw new InvalidOperationException("Invitation has already been accepted");

        if (invitation.Status == InvitationStatus.Rejected)
            throw new InvalidOperationException("Cannot accept a rejected invitation");

        invitation.Status = InvitationStatus.Accepted;
        _invitationRepository.Update(invitation);
        await _invitationRepository.SaveAsync();
    }

    public async Task RejectInvitationAsync(int invitationId)
    {
        var invitation = await _invitationRepository.GetByIdAsync(invitationId);
        if (invitation == null)
            throw new InvalidOperationException("Invitation not found");

        if (invitation.Status == InvitationStatus.Accepted)
            throw new InvalidOperationException("Cannot reject an accepted invitation");

        if (invitation.Status == InvitationStatus.Rejected)
            throw new InvalidOperationException("Invitation has already been rejected");

        invitation.Status = InvitationStatus.Rejected;
        _invitationRepository.Update(invitation);
        await _invitationRepository.SaveAsync();
    }

    public async Task CheckInAsync(int invitationId)
    {
        var invitation = await _invitationRepository.GetInvitationWithDetailsAsync(invitationId);
        if (invitation == null)
            throw new InvalidOperationException("Invitation not found");

        if (invitation.Status != InvitationStatus.Accepted)
            throw new InvalidOperationException("Only accepted invitations can check in");

        var existingParticipation = await _participationRepository.GetByInvitationIdAsync(invitationId);
        if (existingParticipation != null && existingParticipation.CheckInTime.HasValue)
            throw new InvalidOperationException("Already checked in");

        var seatNumber = await _participationRepository.GetNextSeatNumberAsync(invitation.EventId);

        if (existingParticipation == null)
        {
            var participation = new Participation
            {
                InvitationId = invitationId,
                CheckInTime = DateTime.UtcNow,
                SeatNumber = seatNumber
            };

            await _participationRepository.CreateAsync(participation);
        }
        else
        {
            existingParticipation.CheckInTime = DateTime.UtcNow;
            existingParticipation.SeatNumber = seatNumber;
            _participationRepository.Update(existingParticipation);
        }

        await _participationRepository.SaveAsync();
    }

    public async Task<int> GetTotalInvitationsCountAsync()
    {
        var invitations = await _invitationRepository.GetAllAsync();
        return invitations.Count();
    }

    public async Task<int> GetAcceptedInvitationsCountAsync()
    {
        return await _invitationRepository.GetAcceptedInvitationsCountAsync();
    }

    public async Task<int> GetRejectedInvitationsCountAsync()
    {
        return await _invitationRepository.GetRejectedInvitationsCountAsync();
    }
}
