namespace Podium.Application
{
    public class JuryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Experience { get; set; }
        public virtual ICollection<DebateJuriesDto> Debates { get; set; } = new List<DebateJuriesDto>();
        public virtual ICollection<EvaluationDto> Evaluations { get; set; } = new List<EvaluationDto>();
    }
}
