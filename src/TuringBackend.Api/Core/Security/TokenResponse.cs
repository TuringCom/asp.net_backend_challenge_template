namespace TuringBackend.Api.Core
{
    public class TokenResponse : BaseResponse
    {
        public TokenResponse(bool success, string message, AccessToken token) : base(success, message)
        {
            Token = token;
        }

        public AccessToken Token { get; set; }
    }
}