using HelperPE.Persistence.Entities.Users;

namespace HelperPE.Persistence.Repositories
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUsersByCredentials(string email, string password);
    }
}
