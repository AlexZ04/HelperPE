﻿using HelperPE.Common.Enums;
using HelperPE.Common.Models.Curator;
using HelperPE.Common.Models.Profile;
using HelperPE.Infrastructure.Utilities;
using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Extensions
{
    public static class UserConverter
    {
        public static StudentProfileDTO ToDto(this StudentEntity model)
        {
            return new StudentProfileDTO
            {
                Id = model.Id,
                Email = model.Email,
                FullName = model.FullName,
                Role = model.Role,
                Course = model.Course,
                Group = model.Group,
                Faculty = model.Faculty.ToDto(),
                ClassesAmount = CalculateClassesAmount(model),
                AvatarId = model.Avatar?.Id
            };
        }

        private static int CalculateClassesAmount(StudentEntity model)
        {
            var classesAmount = 0;
            var semesterStart = TimeUtility.GetCurrentSemesterStart();
            var semesterEnd = TimeUtility.GetCurrentSemesterEnd();

            foreach (var attendance in model.EventsAttendances)
            {
                if (attendance.Status == EventApplicationStatus.Credited &&
                    attendance.Event.Date >= semesterStart && attendance.Event.Date < semesterEnd)
                {
                    classesAmount += attendance.Event.ClassesAmount;
                }
            }

            foreach (var attendance in model.OtherActivities)
            {
                if (attendance.Date >= semesterStart && attendance.Date < semesterEnd)
                    classesAmount += attendance.ClassesAmount;
            }

            foreach (var attendance in model.PairAttendances)
            {
                if (attendance.Pair.Date >= semesterStart && attendance.Pair.Date < semesterEnd 
                    && attendance.Status == PairAttendanceStatus.Accepted)
                    classesAmount += attendance.ClassesAmount;
            }

            return classesAmount;
        }

        public static StudentProfileShortModel ToShortDto(this StudentEntity model)
        {
            return new StudentProfileShortModel
            {
                Id = model.Id,
                Name = model.FullName,
                Course = model.Course,
                Group = model.Group,
                Role = model.Role,
                AvatarId = model.Avatar?.Id
            };
        }

        public static SportsOrganizerProfileDTO ToDto(this SportsOrganizerEntity model)
        {
            var classesAmount = 0;

            foreach (var attendance in model.EventsAttendances)
            {
                if (attendance.Status == EventApplicationStatus.Credited)
                    classesAmount += attendance.Event.ClassesAmount;
            }

            foreach (var attendance in model.OtherActivities)
                classesAmount += attendance.ClassesAmount;

            classesAmount += model.PairAttendances
                .Where(a => a.Status == PairAttendanceStatus.Accepted).Count();

            return new SportsOrganizerProfileDTO
            {
                Id = model.Id,
                Email = model.Email,
                FullName = model.FullName,
                Role = model.Role,
                Course = model.Course,
                Group = model.Group,
                Faculty = model.Faculty.ToDto(),
                ClassesAmount = classesAmount,
                AppointmentDate = model.AppointmentDate,
                AvatarId = model.Avatar?.Id
            };
        }

        public static TeacherProfileDTO ToDto(this TeacherEntity model)
        {
            return new TeacherProfileDTO
            {
                Id = model.Id,
                Email = model.Email,
                FullName = model.FullName,
                Role = model.Role,
                Subjects = model.Subjects
                                .Select(s => s.ToDto()).ToList(),
                AvatarId = model.Avatar?.Id
            };
        }

        public static TeacherProfileShortModel ToShortDto(this TeacherEntity model)
        {
            return new TeacherProfileShortModel
            {
                Id = model.Id,
                Email = model.Email,
                FullName = model.FullName,
                Role = model.Role,
                AvatarId = model.Avatar?.Id
            };
        }

        public static CuratorProfileDTO ToDto(this CuratorEntity model)
        {
            return new CuratorProfileDTO
            {
                Id = model.Id,
                Email = model.Email,
                FullName = model.FullName,
                Role = model.Role,
                Subjects = model.Subjects
                                .Select(s => s.ToDto()).ToList(),
                Faculties = model.Faculties
                                .Select(f => f.ToDto()).ToList(),
                AvatarId = model.Avatar?.Id
            };
        }
    }
}
