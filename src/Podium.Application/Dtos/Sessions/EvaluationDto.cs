namespace Podium.Application
{
    public class EvaluationDto
    {
        public int Id { get; set; }
        public int JuryId { get; set; }
        public int DebateId { get; set; }
        public int Score { get; set; }
        public string? Observation { get; set; }
        //public virtual JuryDto Jury { get; set; }
        //public virtual DebateDto Debate { get; set; }
    }
}
