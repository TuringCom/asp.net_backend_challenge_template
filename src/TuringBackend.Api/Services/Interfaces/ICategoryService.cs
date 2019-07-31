using System.Collections.Generic;
using System.Threading.Tasks;
using TuringBackend.Api.Core;
using TuringBackend.Models;

namespace TuringBackend.Api.Services
{
    public interface ICategoryService
    {
        Task<PaginatedList<Category>> GetCategoriesAsync(int page = 1, int limit = 20, string sortColumn = null);
        Task<Category> GetCategoryByIdAwait(int id);
        Task<IEnumerable<CategoryBasic>> GetCategoriesByProductIdAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesByDepartmentIdAsync(int id);
    }
}