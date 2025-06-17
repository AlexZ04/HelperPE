using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HelperPE.Common.ProjectSettings
{
    public class AuthOptions
    {
        public const string ISSUER = "HelperPEServer";
        public const string AUDIENCE = "HelperPEClient";
        public const string KEY = "superKeyPuPuPu_52#Peterburg-eeeAaALike";
        public const int LIFETIME_MINUTES = GeneralSettings.ACCESS_TOKEN_LIFETIME;

        public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
