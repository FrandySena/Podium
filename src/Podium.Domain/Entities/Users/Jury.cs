using Podium.Domain.Entities.Sessions;

namespace Podium.Domain.Entities.Users
{
    public class Jury : User
    {
        public string Experience { get; set; }
        public virtual ICollection<DebateJuries> Debates { get; set; } = new List<DebateJuries>();
        public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
