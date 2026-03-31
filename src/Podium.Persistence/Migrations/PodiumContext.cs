using Microsoft.EntityFrameworkCore;
using Podium.Domain.Entities.Sessions;
using Podium.Domain.Entities.Users;

namespace Podium.Persistence.Migrations
{
    public class PodiumContext : DbContext
    {
        public PodiumContext(DbContextOptions<PodiumContext> options) : base(options) {}

        public DbSet<Jury> Juries { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Debate> Debates { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Topic> Topics { get; set; }

        public DbSet<DebateJuries> DebateJuries { get; set; }
        public DbSet<DebateParticipants> DebateParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PodiumContext).Assembly);
        }

    }
}