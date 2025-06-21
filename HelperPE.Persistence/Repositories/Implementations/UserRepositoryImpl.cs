using HelperPE.Common.Constants;
using HelperPE.Common.Exceptions;
using HelperPE.Infrastructure.Utilities;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace HelperPE.Persistence.Repositories.Implementations
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly DataContext _context;
        
        public UserRepositoryImpl(DataContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetUsersByCredentials(string email, string password)
        {
            UserEntity? user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !Hasher.CheckPassword(user.Password, password))
                throw new BadRequestException(ErrorMessages.INVALID_CREDENTIALS);

            return user;
        }
    }
}
