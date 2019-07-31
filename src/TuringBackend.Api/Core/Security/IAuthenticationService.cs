using System.Security.Claims;
using System.Threading.Tasks;

namespace TuringBackend.Api.Core
{
    public interface IAuthenticationService
    {
        string GetUserId(ClaimsPrincipal principal);
        Task<TokenResponse> CreateAccessTokenAsync(string email, string password);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken, string userEmail);
        void RevokeRefreshToken(string refreshToken);

        string CreditCardMask(string cardNumber);
    }
}