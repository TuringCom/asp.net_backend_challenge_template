using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuringBackend.Api.Core;
using TuringBackend.Models;
using TuringBackend.Models.Data;

namespace TuringBackend.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly TuringBackendContext _dbContext;

        public CategoryService(TuringBackendContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<PaginatedList<Category>> GetCategoriesAsync(int page = 1, int limit = 20, string sortColumn = null)
        {
            return await _dbContext
                .Category
                .ToPaginatedListAsync(page, limit, sortColumn);
        }

        public async Task<Category> GetCategoryByIdAwait(int id)
        {
            var category = await _dbContext
                .Category
                .FirstOrDefaultAsync(d => d.CategoryId == id);
            return category;
        }

        public async Task<IEnumerable<CategoryBasic>> GetCategoriesByProductIdAsync(int id)
        {
            var categories = from pg in _dbContext.ProductCategory
                join c in _dbContext.Category on pg.CategoryId equals c.CategoryId
                where pg.ProductId == id
                select new CategoryBasic
                {
                    CategoryId = c.CategoryId,
                    DepartmentId = c.DepartmentId,
                    Name = c.Name
                };

            return await categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByDepartmentIdAsync(int id)
        {
            var categories = from pg in _dbContext.ProductCategory
                join c in _dbContext.Category on pg.CategoryId equals c.CategoryId
                where c.DepartmentId == id
                select new Category
                {
                    CategoryId = c.CategoryId,
                    DepartmentId = c.DepartmentId,
                    Name = c.Name,
                    Description = c.Description
                };

            return await categories.ToListAsync();
        }
    }
}