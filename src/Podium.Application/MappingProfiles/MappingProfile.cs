using AutoMapper;
using Podium.Domain.Entities.Sessions;
using Podium.Domain.Entities.Users;

namespace Podium.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Participant, ParticipantDto>().ReverseMap();
            CreateMap<Jury, JuryDto>().ReverseMap();
            CreateMap<Topic, TopicDto>().ReverseMap();
            CreateMap<Debate, DebateDto>().ReverseMap(); 
            CreateMap<Attendance, AttendanceDto>().ReverseMap();
            CreateMap<Evaluation, EvaluationDto>().ReverseMap();
            CreateMap<DebateParticipants, DebateParticipantsDto>().ReverseMap();
            CreateMap<DebateJuries, DebateJuriesDto>().ReverseMap();
        }
    }
}