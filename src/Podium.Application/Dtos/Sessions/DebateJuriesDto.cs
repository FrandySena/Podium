namespace Podium.Application
{
    public class DebateJuriesDto
    {
        public int Id { get; set; }
        public int DebateId { get; set; }
        public int JuryId { get; set; }
        public virtual DebateDto Debate { get; set; }
        public virtual JuryDto Jury { get; set; }
    }
}
