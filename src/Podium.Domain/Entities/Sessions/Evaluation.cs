using Podium.Domain.Entities.Users;

namespace Podium.Domain.Entities.Sessions
{
    public class Evaluation
    {
        public int Id { get; set; }
        public int JuryId { get; set; }
        public int ParticipantId { get; set; }
        public int DebateId { get; set; }
        public int Score { get; set; }
        public string? Observation { get; set; }
        public virtual Jury Jury { get; set; }
        public virtual Participant Participant { get; set; }
        public virtual Debate Debate { get; set; }
    }
}
