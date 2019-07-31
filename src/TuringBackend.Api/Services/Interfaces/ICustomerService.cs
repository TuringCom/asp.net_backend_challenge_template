using System.Threading.Tasks;
using TuringBackend.Models;

namespace TuringBackend.Api.Services
{
    public interface ICustomerService
    {
        Task<bool> CustomerEmailExists(string email);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<Customer> UpdateCustomerAsync(string email, string address1, string address2, string city, string region, string postalCode, string country, int shippingRegionId);
        Task<Customer> UpdateCustomerCreditCardAsync(string email, string creditCard);
        Task<Customer> UpdateCustomerPasswordAsync(string customerEmail, string newEmail, string name, string password, string dayPhone, string evePhone, string mobPhone);
        Task<Customer> RegisterCustomerAsync(string name, string email, string password);
        bool EmailValid(string email);
    }
}