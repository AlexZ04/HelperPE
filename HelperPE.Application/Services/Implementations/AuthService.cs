using HelperPE.Common.Constants;
using HelperPE.Common.Exceptions;
using HelperPE.Common.Models.Auth;
using HelperPE.Common.ProjectSettings;
using HelperPE.Infrastructure.Utilities;
using HelperPE.Persistence.Contexts;
using HelperPE.Persistence.Entities.Users;
using HelperPE.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace HelperPE.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly DataContext _context;
        private readonly IDistributedCache _tokenCache;

        public AuthService(
            IUserRepository userRepository,
            DataContext context,
            ITokenService tokenService,
            IDistributedCache tokenCache)
        {
            _userRepository = userRepository;
            _context = context;
            _tokenService = tokenService;
            _tokenCache = tokenCache;
        }

        public async Task<TokenResponseModel> Login(LoginUserModel loginModel)
        {
            var user = await _userRepository
                .GetUsersByCredentials(loginModel.Email, loginModel.Password);

            var refreshToken = GetTokenAndAddToDb(user);

            await _tokenService.HandleTokens(user.Id, Guid.Empty);

            await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<TokenResponseModel> RefreshToken(RefreshTokenModel refreshModel)
        {
            var refreshToken = await _context.RefreshTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Token == refreshModel.RefreshToken);

            if (refreshToken == null)
                throw new NotFoundException(ErrorMessages.TOKEN_NOT_FOUND);

            if (!await IsRefreshTokenValid(refreshToken.Token))
                throw new UnauthorizedAccessException();

            refreshToken.Token = _tokenService.GenerateRefreshToken();
            refreshToken.Expires = DateTime.Now.AddDays(GeneralSettings.REFRESH_TOKEN_LIFETIME)
                .ToUniversalTime();

            await _tokenService.HandleTokens(refreshToken.User.Id, refreshToken.Id);
            await _context.SaveChangesAsync();

            string accessToken = _tokenService.GenerateAccessToken(refreshToken.User.Id,
                refreshToken.User.Role.ToString());

            return new TokenResponseModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                AccessExpires = DateTime.Now.AddMinutes(AuthOptions.LIFETIME_MINUTES).ToUniversalTime(),
            };
        }

        public async Task Logout(string? accessToken, Guid userId)
        {
            var refreshToken = await _context.RefreshTokens
                .Include(t => t.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.User.Id == userId);

            if (refreshToken == null || accessToken == null)
                throw new NotFoundException(ErrorMessages.TOKEN_NOT_FOUND);

            await CacheTokens(accessToken, refreshToken.Token);
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

        private async Task CacheTokens(string accessToken, string refreshToken)
        {
            await _tokenCache.SetStringAsync($"blacklist:access:{accessToken}",
                "logout", new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(GeneralSettings.ACCESS_TOKEN_LIFETIME)
            });

            await _tokenCache.SetStringAsync($"blacklist:refresh:{refreshToken}",
                "logout", new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(GeneralSettings.REFRESH_TOKEN_LIFETIME)
            });
        }

        private async Task<bool> IsRefreshTokenValid(string refreshToken)
        {
            string? isTokenCached = await _tokenCache.GetStringAsync($"blacklist:refresh:{refreshToken}");

            if (isTokenCached == null)
                return true;
            return false;
        }
    }
}
