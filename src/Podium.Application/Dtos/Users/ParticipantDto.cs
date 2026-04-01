namespace Podium.Application
{
    public class ParticipantDto : UserDto
    {
        public string Level { get; set; }
        public virtual ICollection<DebateParticipantsDto> Debates { get; set; } = new List<DebateParticipantsDto>();
        public virtual ICollection<EvaluationDto> Evaluations { get; set; } = new List<EvaluationDto>();
        public virtual ICollection<AttendanceDto> Attendances { get; set; } = new List<AttendanceDto>();
    }
}
