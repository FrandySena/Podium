using Microsoft.EntityFrameworkCore;
using Podium.Domain.Entities.Sessions;
using Podium.Domain.Entities.Users;

namespace Podium.Persistence.Migrations
{
    public class PodiumContext : DbContext
    {
        public PodiumContext(DbContextOptions<PodiumContext> options) : base(options) { }

        public DbSet<Jury> Juries { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Debate> Debates { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<DebateJuries> DebateJuries { get; set; }
        public DbSet<DebateParticipants> DebateParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.HasOne(e => e.Jury)
                    .WithMany(j => j.Evaluations)
                    .HasForeignKey(e => e.JuryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Debate)
                .WithMany(d => d.Evaluations)
                .HasForeignKey(e => e.DebateId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DebateParticipants>()
                .HasKey(dp => dp.Id);

            modelBuilder.Entity<DebateJuries>()
                .HasKey(dj => dj.Id);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Participant)
                .WithMany(p => p.Attendances)
                .HasForeignKey(a => a.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}