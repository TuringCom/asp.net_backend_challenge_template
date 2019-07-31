using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuringBackend.Models;
using TuringBackend.Models.Data;

namespace TuringBackend.Api.Services
{
    public class TaxService : ITaxService
    {
        private readonly TuringBackendContext _dbContext;

        public TaxService(TuringBackendContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tax>> GetTaxAsync()
        {
            var taxes = await _dbContext
                .Tax
                .ToListAsync();
            return taxes;
        }

        public async Task<Tax> GetTaxByIdAsync(int id)
        {
            var tax = await _dbContext
                .Tax
                .FirstOrDefaultAsync(d => d.TaxId == id);
            return tax;
        }
    }
}