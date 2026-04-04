namespace Podium.Application
{
    public class DebateDto
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string? Location { get; set; }
        public TopicDto? Topic { get; set; }
        public virtual ICollection<AttendanceDto> Attendances { get; set; } = new List<AttendanceDto>();
        public virtual ICollection<DebateParticipantsDto> Participants { get; set; } = new List<DebateParticipantsDto>();
        public virtual ICollection<DebateJuriesDto> Juries { get; set; } = new List<DebateJuriesDto>();
        public virtual ICollection<EvaluationDto> Evaluations { get; set; } = new List<EvaluationDto>();
    }
}
