using Podium.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Podium.Domain.Entities.Sessions
{
    public class DebateJuries
    {
        [Key]
        public int Id { get; set; }
        public int DebateId { get; set; }
        public int JuryId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual Debate Debate { get; set; }
        public virtual Jury Jury { get; set; }
    }
}
