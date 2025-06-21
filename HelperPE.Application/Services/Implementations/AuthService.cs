using HelperPE.Common.Models.Auth;
using HelperPE.Common.ProjectSettings;
using HelperPE.Infrastructure.Utilities;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Users;
using HelperPE.Persistence.Repositories;

namespace HelperPE.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly DataContext _context;

        public AuthService(
            IUserRepository userRepository,
            DataContext context,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<TokenResponseModel> Login(LoginUserModel loginModel)
        {
            //await AddUser();

            var user = await _userRepository
                .GetUsersByCredentials(loginModel.Email, loginModel.Password);

            var refreshToken = GetTokenAndAddToDb(user);

            await _tokenService.HandleTokens(user.Id, Guid.Empty);

            await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<TokenResponseModel> RefreshToken(RefreshTokenModel refreshModel)
        {
            throw new NotImplementedException();
        }

        public async Task Logout()
        {
            throw new NotImplementedException();
        }

        private TokenResponseModel GetTokenAndAddToDb(UserEntity user)
        {
            RefreshTokenEntity refreshToken = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                User = user,
                Token = _tokenService.GenerateRefreshToken(),
                Expires = DateTime.Now.AddHours(GeneralSettings.REFRESH_TOKEN_LIFETIME)
                    .ToUniversalTime()
            };

            _context.RefreshTokens.Add(refreshToken);

            TokenResponseModel tokenResponseModel = new TokenResponseModel
            {
                AccessToken = _tokenService.GenerateAccessToken(user.Id, user.Role.ToString()),
                RefreshToken = refreshToken.Token,
                AccessExpires = DateTime.Now.AddMinutes(GeneralSettings.ACCESS_TOKEN_LIFETIME)
                    .ToUniversalTime()
            };

            return tokenResponseModel;
        }

        private async Task AddUser()
        {
            var user = new StudentEntity
            {
                Course = 1,
                Group = "972401",
                Faculty = new Persistence.Entities.Faculty.FacultyEntity() { Name = "test " },
                FullName = "First test user",
                Email = "student@example.com",
                Password = Hasher.HashPassword("123456")
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            throw new NotImplementedException();
        }
    }
}
