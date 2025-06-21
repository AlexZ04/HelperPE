namespace HelperPE.Persistence.Entities.Users
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public required UserEntity User { get; set; }
        public DateTime Expires { get; set; } = DateTime.UtcNow;
    }
}
