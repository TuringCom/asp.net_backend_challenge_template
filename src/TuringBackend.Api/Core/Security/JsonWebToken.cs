using System;

namespace TuringBackend.Api.Core
{
    public abstract class JsonWebToken
    {
        public JsonWebToken(string token, long expiration)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Invalid token.");

            if (expiration <= 0)
                throw new ArgumentException("Invalid expiration.");

            Token = token;
            Expiration = expiration;
        }

        public string Token { get; protected set; }
        public long Expiration { get; protected set; }

        public bool IsExpired()
        {
            return DateTime.UtcNow.Ticks > Expiration;
        }
    }
}