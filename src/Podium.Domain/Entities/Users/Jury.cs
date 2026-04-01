using Podium.Domain.Entities.Sessions;

namespace Podium.Domain.Entities.Users
{
    public class Jury
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Experience { get; set; }

        public virtual ICollection<DebateJuries> Debates { get; set; } = new List<DebateJuries>();
        public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
