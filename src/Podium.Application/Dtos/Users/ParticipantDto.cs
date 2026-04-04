namespace Podium.Application
{
    public class ParticipantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Level { get; set; }
        public virtual ICollection<DebateParticipantsDto> Debates { get; set; } = new List<DebateParticipantsDto>();
        public virtual ICollection<AttendanceDto> Attendances { get; set; } = new List<AttendanceDto>();
    }
}
