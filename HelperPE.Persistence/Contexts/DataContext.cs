using HelperPE.Persistence.Entities.Events;
using HelperPE.Persistence.Entities.Faculty;
using HelperPE.Persistence.Entities.Pairs;
using HelperPE.Persistence.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Persistence.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<EventAttendanceEntity> EventsAttendances { get; set; }
        public DbSet<FacultyEntity> Faculties { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<PairEntity> Pairs { get; set; }  
        public DbSet<PairAttendanceEntity> PairsAttendances { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<EventEntity>()
                .HasKey(e => e.EventId);

            modelBuilder.Entity<EventAttendanceEntity>(builder =>
            {
                builder.HasKey(a => new { a.StudentId, a.EventId });

                builder
                    .HasOne(a => a.Student)
                    .WithMany(s => s.EventsAttendances)
                    .HasForeignKey(a => a.StudentId);

                builder
                    .HasOne(a => a.Event)
                    .WithMany(e => e.Attendances)
                    .HasForeignKey(a => a.EventId);
            });

            modelBuilder.Entity<FacultyEntity>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<SubjectEntity>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<PairEntity>()
                .HasKey(p => p.PairId);

            modelBuilder.Entity<PairAttendanceEntity>(builder =>
            {
                builder.HasKey(a => new { a.StudentId, a.PairId });

                builder
                    .HasOne(a => a.Student)
                    .WithMany(s => s.PairAttendances)
                    .HasForeignKey(a => a.StudentId);

                builder
                    .HasOne(a => a.Pair)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(a => a.PairId);

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
