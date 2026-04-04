using Podium.Domain.Entities.Sessions;
using System.ComponentModel.DataAnnotations;

namespace Podium.Domain.Entities.Users
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string LastName { get; set; }
        public int Age { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(int.MaxValue)]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Level { get; set; }

        public virtual ICollection<DebateParticipants> Debates { get; set; } = new List<DebateParticipants>();
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
