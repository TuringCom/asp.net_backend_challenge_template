using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuringBackend.Models;
using TuringBackend.Models.Data;

namespace TuringBackend.Api.Services
{
    public class ShippingService : IShippingService
    {
        private readonly TuringBackendContext _dbContext;

        public ShippingService(TuringBackendContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Shipping>> GetShippingAsync()
        {
            var shippings = await _dbContext
                .Shipping
                .ToListAsync();
            return shippings;
        }

        public async Task<Shipping> GetShippingByIdAsync(int id)
        {
            var shipping = await _dbContext
                .Shipping
                .FirstOrDefaultAsync(d => d.ShippingId == id);
            return shipping;
        }
    }
}