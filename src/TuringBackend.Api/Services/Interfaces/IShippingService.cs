using System.Collections.Generic;
using System.Threading.Tasks;
using TuringBackend.Models;

namespace TuringBackend.Api.Services
{
    public interface IShippingService
    {
        Task<IEnumerable<Shipping>> GetShippingAsync();
        Task<Shipping> GetShippingByIdAsync(int id);
    }
}