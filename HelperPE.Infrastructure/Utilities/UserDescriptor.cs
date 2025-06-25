using System.Security.Claims;

namespace HelperPE.Infrastructure.Utilities
{
    public class UserDescriptor
    {
        public static Guid GetUserId(ClaimsPrincipal principal)
        {
            string? userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new UnauthorizedAccessException();

            return new Guid(userId);
        }

        public static string GetUserRole(ClaimsPrincipal principal)
        {
            string? userRole = principal.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == null)
                throw new UnauthorizedAccessException();

            return userRole;
        }
    }
}
