namespace Podium.Application
{
    public class DebateParticipantsDto
    {
        public int Id { get; set; }
        public int DebateId { get; set; }
        public int ParticipantId { get; set; }
        public string Role { get; set; }
        //public virtual DebateDto Debate { get; set; }
        //public virtual ParticipantDto Participant { get; set; }
    }
}
