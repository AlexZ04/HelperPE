﻿using HelperPE.Common.Enums;

namespace HelperPE.Common.Models.Profile
{
    public class SportsOrganizerProfileDTO
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public UserRole Role { get; set; }
        public required int Course { get; set; }
        public required string Group { get; set; }
        public required FacultyDTO Faculty { get; set; }
        public required int ClassesAmount { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Guid? AvatarId { get; set; }
    }
}
