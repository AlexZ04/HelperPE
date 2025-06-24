using HelperPE.Common.Enums;
using HelperPE.Infrastructure.Utilities;
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
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserEntity>()
            //    .HasKey(u => u.Id);
            modelBuilder.Entity<UserEntity>()
                .HasDiscriminator<string>("UserType")
                .HasValue<TeacherEntity>("Teacher")
                .HasValue<CuratorEntity>("Curator")
                .HasValue<SportsOrganizerEntity>("SportsOrganizer")
                .HasValue<StudentEntity>("Student");
                //? нужен админ

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

            modelBuilder.Entity<RefreshTokenEntity>()
                .HasKey(t => t.Id);


            var basketball = new SubjectEntity()
            {
                Id = Guid.Parse("6a541e68-cd4c-45bc-94fb-97634ef8a3ef"),
                Name = "Баскетбол"
            };

            modelBuilder.Entity<SubjectEntity>().HasData(basketball);
            TeacherEntity peTeacher = new TeacherEntity()
            {
                Id = Guid.Parse("1ea30ff4-00c9-44f9-afb9-651471a366f6"),
                Email = "peteacher@example.com",
                FullName = "Thomas Zane",
                Password = "$2a$11$Ug2z7Jxu7srXwiGMEuqfK.MW7uXoH.hP/VsjtygCSobdtJwldDl/q"

            };

            modelBuilder.Entity<TeacherEntity>().HasData(peTeacher);

            modelBuilder.Entity("SubjectEntityTeacherEntity").HasData(
                new { SubjectsId = basketball.Id, TeachersId = peTeacher.Id }
            );
            modelBuilder.Entity<FacultyEntity>().HasData(
                new FacultyEntity { Id = Guid.Parse("12345678-1234-1234-1234-123456789012"), Name = "Отдел подготовки кадров высшей квалификации" },
                new FacultyEntity { Id = Guid.Parse("23456789-2345-2345-2345-234567890123"), Name = "Факультет иностранных языков" },
                new FacultyEntity { Id = Guid.Parse("34567890-3456-3456-3456-345678901234"), Name = "САЕ Институт «Умные материалы и технологии»" },
                new FacultyEntity { Id = Guid.Parse("3f339655-3c00-4c8d-991e-7708eb5bee6c"), Name = "НОЦ «Высшая ИТ-Школа»" }

            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
