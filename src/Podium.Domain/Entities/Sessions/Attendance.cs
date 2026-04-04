using Podium.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Podium.Domain.Entities.Sessions
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int DebateId { get; set; }
        public bool IsPresent { get; set; }
        public virtual Participant Participant { get; set; }
        public virtual Debate Debate { get; set; }
    }
}
