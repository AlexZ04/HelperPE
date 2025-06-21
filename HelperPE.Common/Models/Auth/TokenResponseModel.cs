namespace HelperPE.Common.Models.Auth
{
    public class TokenResponseModel
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessExpires { get; set; } = DateTime.UtcNow;
    }
}
