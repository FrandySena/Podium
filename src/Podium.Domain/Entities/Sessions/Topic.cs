namespace Podium.Domain.Entities.Sessions
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Debate> Debates { get; set; } = new List<Debate>();
    }
}
