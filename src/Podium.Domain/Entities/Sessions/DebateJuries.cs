using Podium.Domain.Entities.Users;

namespace Podium.Domain.Entities.Sessions
{
    public class DebateJuries
    {
        public int Id { get; set; }
        public int DebateId { get; set; }
        public int JuryId { get; set; }
        public virtual Debate Debate { get; set; }
        public virtual Jury Jury { get; set; }
    }
}
