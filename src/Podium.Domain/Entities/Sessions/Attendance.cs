using Podium.Domain.Entities.Users;

namespace Podium.Domain.Entities.Sessions
{
    public class Attendance
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int DebateId { get; set; }
        public bool IsPresent { get; set; }
        public virtual Participant Participant { get; set; }
        public virtual Debate Debate { get; set; }
    }
}
