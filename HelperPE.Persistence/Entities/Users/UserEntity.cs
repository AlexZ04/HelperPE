using HelperPE.Common.Enums;

namespace HelperPE.Persistence.Entities.Users
{
    public abstract class UserEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public UserRole Role { get; set; }
        public required string Password { get; set; }

        public FileEntity? Avatar { get; set; }
        public UserEntity(UserRole role)
        {
            Role = role;
        }
    }
}
