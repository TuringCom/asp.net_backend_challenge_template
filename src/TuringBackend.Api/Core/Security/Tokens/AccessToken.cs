using System;

namespace TuringBackend.Api.Core
{
    public class AccessToken : JsonWebToken
    {
        public AccessToken(string token, long expiration, RefreshToken refreshToken) : base(token, expiration)
        {
            if (refreshToken == null)
                throw new ArgumentException("Specify a valid refresh token.");

            RefreshToken = refreshToken;
        }

        public RefreshToken RefreshToken { get; }
    }
}