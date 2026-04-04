using Podium.Domain.Entities.Sessions;

namespace Podium.Domain.Entities.Users
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Level { get; set; }

        public virtual ICollection<DebateParticipants> Debates { get; set; } = new List<DebateParticipants>();
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
