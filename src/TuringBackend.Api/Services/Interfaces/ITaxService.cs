using System.Collections.Generic;
using System.Threading.Tasks;
using TuringBackend.Models;

namespace TuringBackend.Api.Services
{
    public interface ITaxService
    {
        Task<IEnumerable<Tax>> GetTaxAsync();
        Task<Tax> GetTaxByIdAsync(int id);
    }
}