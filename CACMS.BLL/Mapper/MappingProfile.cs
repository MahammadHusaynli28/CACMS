using AutoMapper;
using CACMS.BLL.DTOs.EventDTOs;
using CACMS.BLL.DTOs.EventTypeDTOs;
using CACMS.BLL.DTOs.InvitationDTOs;
using CACMS.BLL.DTOs.LocationDTOs;
using CACMS.DAL.Entities;
using CACMS.DAL.Entities.Enums;

namespace CACMS.BLL.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Event Mappings
        CreateMap<Event, GetEventDTO>()
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location!.Name))
            .ForMember(dest => dest.EventTypeName, opt => opt.MapFrom(src => src.EventType!.Name))
            .ForMember(dest => dest.OrganizerName, opt => opt.MapFrom(src => $"{src.Organizer!.FirstName} {src.Organizer!.LastName}"))
            .ForMember(dest => dest.InvitationCount, opt => opt.MapFrom(src => src.Invitations.Count))
            .ForMember(dest => dest.AcceptedCount, opt => opt.MapFrom(src => src.Invitations.Count(i => i.Status == InvitationStatus.Accepted)))
            .ForMember(dest => dest.ParticipantCount, opt => opt.MapFrom(src => src.Invitations.Count(i => i.Participation != null && i.Participation.CheckInTime.HasValue)));

        CreateMap<Event, ListEventDTO>()
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location!.Name))
            .ForMember(dest => dest.EventTypeName, opt => opt.MapFrom(src => src.EventType!.Name))
            .ForMember(dest => dest.InvitationCount, opt => opt.MapFrom(src => src.Invitations.Count));

        CreateMap<CreateEventDTO, Event>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UpdateEventDTO, Event>();

        // EventType Mappings
        CreateMap<EventType, GetEventTypeDTO>()
            .ForMember(dest => dest.EventCount, opt => opt.MapFrom(src => src.Events.Count));

        CreateMap<EventType, ListEventTypeDTO>();
        CreateMap<CreateEventTypeDTO, EventType>();
        CreateMap<UpdateEventTypeDTO, EventType>();

        // Location Mappings
        CreateMap<Location, GetLocationDTO>()
            .ForMember(dest => dest.EventCount, opt => opt.MapFrom(src => src.Events.Count));

        CreateMap<Location, ListLocationDTO>();
        CreateMap<CreateLocationDTO, Location>();
        CreateMap<UpdateLocationDTO, Location>();

        // Invitation Mappings
        CreateMap<Invitation, GetInvitationDTO>()
            .ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.Event!.Title))
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.Event!.Date))
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Event!.Location!.Name))
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => $"{src.Person!.FirstName} {src.Person!.LastName}"))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.HasParticipation, opt => opt.MapFrom(src => src.Participation != null))
            .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Participation != null ? src.Participation.SeatNumber : null))
            .ForMember(dest => dest.CheckInTime, opt => opt.MapFrom(src => src.Participation != null ? src.Participation.CheckInTime : null));

        CreateMap<Invitation, ListInvitationDTO>()
            .ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.Event!.Title))
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.Event!.Date))
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => $"{src.Person!.FirstName} {src.Person!.LastName}"))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<CreateInvitationDTO, Invitation>()
            .ForMember(dest => dest.SentAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => InvitationStatus.Pending));
    }
}
