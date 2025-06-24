using HelperPE.Common.Enums;

namespace HelperPE.Persistence.Entities.Users
{
    public class AdminEntity : UserEntity
    {
        public AdminEntity() : base(UserRole.Admin) { }
    }
}
