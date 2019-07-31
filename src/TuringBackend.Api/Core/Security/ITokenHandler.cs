using TuringBackend.Models;

namespace TuringBackend.Api.Core
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(Customer customer);
        RefreshToken TakeRefreshToken(string token);
        void RevokeRefreshToken(string token);
    }
}