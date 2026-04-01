using Podium.Domain.Entities.Sessions;

namespace Podium.Application
{
    public class TopicDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<DebateDto> Debates { get; set; } = new List<DebateDto>();
    }
}
