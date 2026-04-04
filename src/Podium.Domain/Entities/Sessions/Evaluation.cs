using Podium.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Podium.Domain.Entities.Sessions
{
    public class Evaluation
    {
        [Key]
        public int Id { get; set; }
        public int JuryId { get; set; }
        public int DebateId { get; set; }
        public int Score { get; set; }

        [StringLength(500)]
        public string? Observation { get; set; }
        public virtual Jury Jury { get; set; }
        public virtual Debate Debate { get; set; }
    }
}