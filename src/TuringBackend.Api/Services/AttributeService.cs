using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuringBackend.Models;
using TuringBackend.Models.Data;

namespace TuringBackend.Api.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly TuringBackendContext _dbContext;

        public AttributeService(TuringBackendContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Attribute>> GetAttributesAsync()
        {
            var attributes = await _dbContext
                .Attribute
                .ToListAsync();
            return attributes;
        }

        public async Task<Attribute> GetAttributeByIdAsync(int id)
        {
            var attribute = await _dbContext
                .Attribute
                .FirstOrDefaultAsync(d => d.AttributeId == id);
            return attribute;
        }

        public async Task<IEnumerable<AttributeValueByAttribute>> GetAttributeValueByAttributeIdAsync(int id)
        {
            var attribute = await _dbContext
                .AttributeValue
                .Where(d => d.AttributeId == id)
                .Select(a => new AttributeValueByAttribute
                {
                    AttributeValueId = a.AttributeValueId,
                    Value = a.Value
                })
                .ToListAsync();
            return attribute;
        }

        public async Task<IEnumerable<ProductAttributeValue>> GetAttributeValueByProductIdAsync(int id)
        {
            var productAttributeValues = from pa in _dbContext.ProductAttribute
                join av in _dbContext.AttributeValue on pa.AttributeValueId equals av.AttributeValueId
                join a in _dbContext.Attribute on av.AttributeId equals a.AttributeId
                where pa.ProductId == id
                select new ProductAttributeValue
                {
                    AttributeValueId = av.AttributeValueId,
                    AttributeValue = av.Value,
                    AttributeName = a.Name
                };

            return await productAttributeValues.ToListAsync();
        }
    }
}