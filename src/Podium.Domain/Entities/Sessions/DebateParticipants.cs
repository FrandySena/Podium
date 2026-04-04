using Podium.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Podium.Domain.Entities.Sessions
{
    public class DebateParticipants
    {
        [Key]
        public int Id { get; set; }
        public int DebateId { get; set; }
        public int ParticipantId { get; set; }

        [StringLength(100)]
        public string Role { get; set; }
        public virtual Debate Debate { get; set; }
        public virtual Participant Participant { get; set; }
    }
}
