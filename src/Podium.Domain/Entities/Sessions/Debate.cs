namespace Podium.Domain.Entities.Sessions
{
    public class Debate
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string? Location { get; set; }
        public Topic Topic { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public virtual ICollection<DebateParticipants> Participants { get; set; } = new List<DebateParticipants>();
        public virtual ICollection<DebateJuries> Juries { get; set; } = new List<DebateJuries>();
        public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
