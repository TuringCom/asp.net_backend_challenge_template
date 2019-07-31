namespace TuringBackend.Api.Services
{
    public interface ICreditCardService
    {
        bool IsCardNumberValid(string creditCardNumber);
    }
}