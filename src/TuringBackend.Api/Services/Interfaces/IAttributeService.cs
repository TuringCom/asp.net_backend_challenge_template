using System.Collections.Generic;
using System.Threading.Tasks;
using TuringBackend.Models;

namespace TuringBackend.Api.Services
{
    public interface IAttributeService
    {
        Task<IEnumerable<Attribute>> GetAttributesAsync();
        Task<Attribute> GetAttributeByIdAsync(int id);
        Task<IEnumerable<AttributeValueByAttribute>> GetAttributeValueByAttributeIdAsync(int id);
        Task<IEnumerable<ProductAttributeValue>> GetAttributeValueByProductIdAsync(int id);
    }
}