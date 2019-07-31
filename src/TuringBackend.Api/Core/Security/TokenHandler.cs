using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using TuringBackend.Models;

namespace TuringBackend.Api.Core
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();
        private readonly SigningConfigurations _signingConfigurations;

        private readonly TokenOptions _tokenOptions;

        public TokenHandler(IOptions<TokenOptions> tokenOptionsSnapshot, SigningConfigurations signingConfigurations,
            IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _tokenOptions = tokenOptionsSnapshot.Value;
            _signingConfigurations = signingConfigurations;
        }

        public AccessToken CreateAccessToken(Customer customer)
        {
            var refreshToken = BuildRefreshToken(customer);
            var accessToken = BuildAccessToken(customer, refreshToken);
            _refreshTokens.Add(refreshToken);

            return accessToken;
        }

        public RefreshToken TakeRefreshToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var refreshToken = _refreshTokens.SingleOrDefault(t => t.Token == token);
            if (refreshToken != null)
                _refreshTokens.Remove(refreshToken);

            return refreshToken;
        }

        public void RevokeRefreshToken(string token)
        {
            TakeRefreshToken(token);
        }

        private RefreshToken BuildRefreshToken(Customer customer)
        {
            var refreshToken = new RefreshToken
            (
                _passwordHasher.HashPassword(Guid.NewGuid().ToString()),
                DateTime.UtcNow.AddHours(_tokenOptions.RefreshTokenExpiration).Ticks
            );

            return refreshToken;
        }

        private AccessToken BuildAccessToken(Customer customer, RefreshToken refreshToken)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddHours(_tokenOptions.AccessTokenExpiration);

            var securityToken = new JwtSecurityToken
            (
                _tokenOptions.Issuer,
                _tokenOptions.Audience,
                GetClaims(customer),
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: _signingConfigurations.SigningCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken);
        }

        private IEnumerable<Claim> GetClaims(Customer customer)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, customer.Email)
            };

            // TODO: This is because we do not have any other users except customers
            claims.Add(new Claim(ClaimTypes.Role, "customer"));

            return claims;
        }
    }
}