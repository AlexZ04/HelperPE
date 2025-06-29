using HelperPE.Common.Enums;
using HelperPE.Infrastructure.Utilities;
using HelperPE.Persistence.Entities.Faculty;
using HelperPE.Persistence.Entities.Pairs;
using HelperPE.Persistence.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Persistence.Contexts
{
    public static class DataSeeder
    {
        public async static Task Seed(DataContext context)
        {
            var basketballId = new Guid("6a541e68-cd4c-45bc-94fb-97634ef8a3ef");
            var basketball = context.Subjects
                .Include(s => s.Teachers)
                .FirstOrDefault(s => s.Id == basketballId);
            if (basketball == null)
            {
                basketball = new SubjectEntity
                {
                    Id = basketballId,
                    Name = "Баскетбол"
                };
                context.Subjects.Add(basketball);
            }

            var fitnessId = new Guid("d718b0d0-16a1-4d74-9be3-8d98d3af2bb3");
            var fitness = context.Subjects
                .Include(s => s.Teachers)
                .FirstOrDefault(s => s.Id == fitnessId);
            if (fitness == null)
            {
                fitness = new SubjectEntity
                {
                    Id = fitnessId,
                    Name = "Фитнес"
                };
                context.Subjects.Add(fitness);
            }

            var peTeacherId = new Guid("1ea30ff4-00c9-44f9-afb9-651471a366f6");
            var peTeacher = context.Users.OfType<TeacherEntity>()
                .FirstOrDefault(t => t.Id == peTeacherId);
            if (peTeacher == null)
            {
                peTeacher = new TeacherEntity
                {
                    Id = peTeacherId,
                    Email = "peteacher@example.com",
                    FullName = "Thomas Zane",
                    Password = Hasher.HashPassword("string")
                };
                context.Users.Add(peTeacher);
            }

            var adminId = new Guid("9dc037bf-624a-464e-84d0-4ff4c1ae0529");
            var admin = context.Users.OfType<AdminEntity>()
                .FirstOrDefault(t => t.Id == adminId);
            if (admin == null)
            {
                admin = new AdminEntity
                {
                    Id = adminId,
                    Email = "admin@example.com",
                    FullName = "PE God",
                    Password = Hasher.HashPassword("string")
                };
                context.Users.Add(admin);
            }

            var faculty1Id = new Guid("12345678-1234-1234-1234-123456789012");
            var faculty2Id = new Guid("23456789-2345-2345-2345-234567890123");
            var faculty3Id = new Guid("34567890-3456-3456-3456-345678901234");
            var faculty4Id = new Guid("3f339655-3c00-4c8d-991e-7708eb5bee6c");
            
            var HITs = context.Faculties.FirstOrDefault(f => f.Id == faculty4Id);
            if (HITs == null)
                HITs = new FacultyEntity { Id = faculty4Id, Name = "НОЦ «Высшая ИТ-Школа»" };

            if (!context.Faculties.Any())
            {
                context.Faculties.AddRange(
                    new FacultyEntity { Id = faculty1Id, Name = "Факультет журналистики" },
                    new FacultyEntity { Id = faculty2Id, Name = "Факультет иностранных языков" },
                    new FacultyEntity { Id = faculty3Id, Name = "Радиофизический факультет" },
                    HITs
                );
            }

            var curatorId = new Guid("0ac0389b-b5db-482b-a5ff-957a1cad4dec");
            var curator = context.Users.OfType<CuratorEntity>()
                .FirstOrDefault(c => c.Id == curatorId);
            if (curator == null)
            {
                curator = new CuratorEntity
                {
                    Id = curatorId,
                    Email = "curator@example.com",
                    FullName = "Disco potato",
                    Password = Hasher.HashPassword("string"),
                    Faculties = new List<FacultyEntity>() { HITs }
                };
                context.Users.Add(curator);
            }

            var studentId = new Guid("93c152cf-e080-4b70-9c9b-39097e768944");
            var student = context.Users.OfType<StudentEntity>()
                .FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                student = new StudentEntity
                {
                    Id = studentId,
                    Course = 1,
                    Group = "972401",
                    Faculty = HITs,
                    FullName = "PE lover",
                    Email = "student@example.com",
                    Password = Hasher.HashPassword("string"),
                    Role = UserRole.Student
                };
                context.Users.Add(student);
            }

            var student2Id = new Guid("57bd840f-09b8-4ce4-9350-4df1dd695643");
            var student2 = context.Users.OfType<StudentEntity>()
                .FirstOrDefault(s => s.Id == student2Id);
            if (student2 == null)
            {
                student2 = new StudentEntity
                {
                    Id = student2Id,
                    Course = 1,
                    Group = "972401",
                    Faculty = HITs,
                    FullName = "PE lover 2",
                    Email = "student2@example.com",
                    Password = Hasher.HashPassword("string"),
                    Role = UserRole.Student
                };
                context.Users.Add(student2);
            }

            var student3Id = new Guid("8fcb6229-d955-4220-8e8a-9bfe56be0d1e");
            var student3 = context.Users.OfType<StudentEntity>()
                .FirstOrDefault(s => s.Id == student3Id);
            if (student3 == null)
            {
                student3 = new StudentEntity
                {
                    Id = student3Id,
                    Course = 1,
                    Group = "972401",
                    Faculty = HITs,
                    FullName = "PE lover 3",
                    Email = "student3@example.com",
                    Password = Hasher.HashPassword("string"),
                    Role = UserRole.Student
                };
                context.Users.Add(student3);
            }

            var sportsOrganizerId = new Guid("72b5c6ad-501b-4f17-b0d6-35384b589154");
            var sportsOrganizer = context.Users.OfType<SportsOrganizerEntity>()
                .FirstOrDefault(s => s.Id == sportsOrganizerId);
            if (sportsOrganizer == null)
            {
                sportsOrganizer = new SportsOrganizerEntity
                {
                    Id = sportsOrganizerId,
                    Course = 2,
                    Group = "972302",
                    Faculty = HITs,
                    FullName = "PE master",
                    Email = "sports@example.com",
                    Password = Hasher.HashPassword("string"),
                    AppointmentDate = DateTime.UtcNow,
                    Role = UserRole.SportsOrganizer
                };
                context.Users.Add(sportsOrganizer);
            }

            if (!basketball.Teachers.Any(t => t.Id == peTeacherId))
                basketball.Teachers.Add(peTeacher);
            if (!basketball.Teachers.Any(t => t.Id == curatorId))
                basketball.Teachers.Add(curator);
            if (!fitness.Teachers.Any(t => t.Id == peTeacherId))
                fitness.Teachers.Add(peTeacher);

            await context.SaveChangesAsync();
        }
    }
} 