using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TuringBackend.Models.Data;

namespace TuringBackend.Api.Core
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly TuringBackendContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(TuringBackendContext dbContext, IPasswordHasher passwordHasher,
            ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
        }

        public string GetUserId(ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<TokenResponse> CreateAccessTokenAsync(string email, string password)
        {
            var customer = await _dbContext
                .Customer
                .FirstAsync(c => c.Email == email);

            if (customer == null || !_passwordHasher.PasswordMatches(password, customer.Password))
                return new TokenResponse(false, "Invalid credentials.", null);

            var token = _tokenHandler.CreateAccessToken(customer);

            return new TokenResponse(true, null, token);
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, string customerEmail)
        {
            var token = _tokenHandler.TakeRefreshToken(refreshToken);

            if (token == null) return new TokenResponse(false, "Invalid refresh token.", null);

            if (token.IsExpired()) return new TokenResponse(false, "Expired refresh token.", null);

            var customer = await _dbContext
                .Customer
                .FirstAsync(c => c.Email == customerEmail);

            if (customer == null) return new TokenResponse(false, "Invalid refresh token.", null);

            var accessToken = _tokenHandler.CreateAccessToken(customer);
            return new TokenResponse(true, null, accessToken);
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            _tokenHandler.RevokeRefreshToken(refreshToken);
        }

        public string CreditCardMask(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return string.Empty;

            var firstDigits = cardNumber.Substring(0, 6);
            var lastDigits = cardNumber.Substring(cardNumber.Length - 4, 4);
            var requiredMask = new String('X', cardNumber.Length - firstDigits.Length - lastDigits.Length);
            var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
            return Regex.Replace(maskedString, ".{4}", "$0 ");
        }
    }
}