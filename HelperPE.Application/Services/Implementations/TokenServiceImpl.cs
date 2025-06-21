using HelperPE.Common.ProjectSettings;
using HelperPE.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace HelperPE.Application.Services.Implementations
{
    public class TokenServiceImpl : ITokenService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler
            = new JwtSecurityTokenHandler();
        private readonly DataContext _context;

        public TokenServiceImpl(DataContext context)
        {
            _context = context;
        }

        public string GenerateAccessToken(Guid id, string role)
        {
            ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Role, role)
                });

            var tokenDescriptor = CreateTokenDescriptor(claims);

            var token = _tokenHandler.CreateToken(tokenDescriptor);

            return _tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        private SecurityTokenDescriptor CreateTokenDescriptor(ClaimsIdentity claims)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Issuer = AuthOptions.ISSUER,
                Audience = AuthOptions.AUDIENCE,
                Expires = DateTime.Now.AddMinutes(AuthOptions.LIFETIME_MINUTES).ToUniversalTime(),
                SigningCredentials = new(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenDescriptor;
        }

        public async Task HandleTokens(Guid userId, Guid tokenId)
        {
            await _context.RefreshTokens
                .Include(t => t.User)
                .Where(t => t.User.Id == userId && t.Id != tokenId)
                .ExecuteDeleteAsync();
        }
    }
}
