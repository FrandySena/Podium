using Podium.Domain.Entities.Sessions;
using Podium.Domain.Entities.Users;

namespace Podium.Application
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int DebateId { get; set; }
        public bool IsPresent { get; set; }
        //public virtual ParticipantDto Participant { get; set; }
        //public virtual DebateDto Debate { get; set; }
    }
}
