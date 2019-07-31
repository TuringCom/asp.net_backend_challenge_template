using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuringBackend.Api.Core;
using TuringBackend.Models;
using TuringBackend.Models.Data;

namespace TuringBackend.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private const string EmailPattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
        private const RegexOptions EmailRegExOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

        private readonly TuringBackendContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public CustomerService(TuringBackendContext dbContext,
            IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public bool EmailValid(string email)
        {
            return Regex.IsMatch(email, EmailPattern, EmailRegExOptions);
        }

        public async Task<bool> CustomerEmailExists(string email)
        {
            var customer = await _dbContext
                .Customer
                .FirstOrDefaultAsync(c => c.Email == email);
            return customer != null;
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            var customer = await _dbContext
                .Customer
                .FirstOrDefaultAsync(c => c.Email == email);
            return customer;
        }

        public async Task<Customer> UpdateCustomerAsync(string email, string address1, string address2, string city, string region, string postalCode, string country, int shippingRegionId)
        {
            var customer = await _dbContext
                .Customer
                .FirstOrDefaultAsync(c => c.Email == email);

            customer.Address1 = address1;
            customer.Address2 = address2;
            customer.City = city;
            customer.Region = region;
            customer.PostalCode = postalCode;
            customer.Country = country;
            customer.ShippingRegionId = shippingRegionId;

            _dbContext.Entry(customer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> UpdateCustomerCreditCardAsync(string email, string creditCard)
        {
            var customer = await _dbContext
                .Customer
                .FirstOrDefaultAsync(c => c.Email == email);
            customer.CreditCard = creditCard;
            _dbContext.Entry(customer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> UpdateCustomerPasswordAsync(string customerEmail, string newEmail,  string name, string password, string dayPhone, string evePhone, string mobPhone)
        {
            var customer = await _dbContext
                .Customer
                .FirstOrDefaultAsync(c => c.Email == customerEmail);

            customer.Email = newEmail ?? customerEmail;
            customer.Name = name;
            customer.DayPhone = dayPhone;
            customer.EvePhone = evePhone;
            customer.MobPhone = mobPhone;
            // Just to avoid having a blank password
            if (!string.IsNullOrWhiteSpace(password))
            {
                customer.Password = _passwordHasher.HashPassword(password);
            }

            _dbContext.Entry(customer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> RegisterCustomerAsync(string name, string email, string password)
        {
            var customer = new Customer
            {
                Name = name,
                Email = email,
                Password = _passwordHasher.HashPassword(password)
            };
            await _dbContext.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }
    }
}