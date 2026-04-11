using Podium.Domain.Entities.Sessions;
using Podium.Domain.Entities.Users;
using Podium.Persistence.Migrations;

namespace Podium.Infrastructure.Respositories
{
    public class UnitOfWork
    {
        private readonly PodiumContext _context;

        private readonly GenericRepository<Jury> _juryRepository;
        private readonly GenericRepository<Participant> _participantRepository;

        private readonly GenericRepository<Attendance> _attendanceRepository;
        private readonly GenericRepository<Debate> _debateRepository;
        private readonly GenericRepository<Topic> _topicRepository;
        private readonly GenericRepository<Evaluation> _evaluationRepository;
        private readonly GenericRepository<DebateParticipants> _debateParticipantsRepository;
        private readonly GenericRepository<DebateJuries> _debateJuriesRepository;

        public UnitOfWork(PodiumContext context,
            GenericRepository<Jury> juryRepository,
            GenericRepository<Participant> participantRepository,
            GenericRepository<Attendance> attendanceRepository,
            GenericRepository<Debate> debateRepository,
            GenericRepository<DebateJuries> debateJuriesRepository,
            GenericRepository<DebateParticipants> debateParticipantsRepository,
            GenericRepository<Evaluation> evaluationRepository,
            GenericRepository<Topic> topicRepository
            )
        {
            _context = context;
            _juryRepository = juryRepository;
            _participantRepository = participantRepository;
            _attendanceRepository = attendanceRepository;
            _debateRepository = debateRepository;
            _debateJuriesRepository = debateJuriesRepository;
            _debateParticipantsRepository = debateParticipantsRepository;
            _evaluationRepository = evaluationRepository;
            _topicRepository = topicRepository;
        }

        public GenericRepository<Jury> JuryRepository => _juryRepository;
        public GenericRepository<Participant> ParticipantRepository => _participantRepository;
        public GenericRepository<Attendance> AttendanceRepository => _attendanceRepository;
        public GenericRepository<Debate> DebateRepository => _debateRepository;
        public GenericRepository<DebateJuries> DebateJuries => _debateJuriesRepository;
        public GenericRepository<DebateParticipants> DebateParticipants => _debateParticipantsRepository;
        public GenericRepository<Evaluation> EvaluationsRepository => _evaluationRepository;
        public GenericRepository<Topic> Topics => _topicRepository;

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransaction()
        {
            await _context.Database.CommitTransactionAsync();
        }
        public async Task RollbackTransaction()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
