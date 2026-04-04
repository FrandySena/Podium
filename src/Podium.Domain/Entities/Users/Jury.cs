using Podium.Domain.Entities.Sessions;
using System.ComponentModel.DataAnnotations;

namespace Podium.Domain.Entities.Users
{
    public class Jury
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

        [StringLength(500)]
        public string Experience { get; set; }

        public virtual ICollection<DebateJuries> Debates { get; set; } = new List<DebateJuries>();
        public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
