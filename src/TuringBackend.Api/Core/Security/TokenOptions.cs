namespace TuringBackend.Api.Core
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }

        // Expiration in hours
        public long AccessTokenExpiration { get; set; }
        public long RefreshTokenExpiration { get; set; }
    }
}