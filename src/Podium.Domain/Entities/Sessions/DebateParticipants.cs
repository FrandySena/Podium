using Podium.Domain.Entities.Users;

namespace Podium.Domain.Entities.Sessions
{
    public class DebateParticipants
    {
        public int Id { get; set; }
        public int DebateId { get; set; }
        public int ParticipantId { get; set; }
        public string Role { get; set; }
        public virtual Debate Debate { get; set; }
        public virtual Participant Participant { get; set; }
    }
}
