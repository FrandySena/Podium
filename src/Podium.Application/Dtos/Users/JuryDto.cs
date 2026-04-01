using Podium.Domain.Entities.Sessions;

namespace Podium.Application
{
    public class JuryDto : UserDto
    {
        public string Experience { get; set; }
        public virtual ICollection<DebateJuriesDto> Debates { get; set; } = new List<DebateJuriesDto>();
        public virtual ICollection<EvaluationDto> Evaluations { get; set; } = new List<EvaluationDto>();
    }
}
