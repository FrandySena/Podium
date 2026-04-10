using System.ComponentModel.DataAnnotations;

namespace Podium.Domain.Entities.Sessions
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Debate> Debates { get; set; } = new List<Debate>();
    }
}
