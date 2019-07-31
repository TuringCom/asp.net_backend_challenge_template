namespace TuringBackend.Api.Core
{
    public abstract class BaseResponse
    {
        public BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; protected set; }
        public string Message { get; protected set; }
    }
}