using Podium.Domain.Entities.Sessions;

namespace Podium.Domain.Entities.Users
{
    public class Participant : User
    {
        public string Level { get; set; }
        public virtual ICollection<DebateParticipants> Debates { get; set; } = new List<DebateParticipants>();
        public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
