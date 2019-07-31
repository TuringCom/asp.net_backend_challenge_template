using System.Collections.Generic;
using System.Threading.Tasks;
using TuringBackend.Api.Core;
using TuringBackend.Models;

namespace TuringBackend.Api.Services
{
    public interface IProductService
    {
        Task<PaginatedList<Product>> GetProductsASync(int page = 1, int limit = 20, string sortColumn = null);
        Task<Product> GetProductByIdAsync(int id);

        Task<PaginatedList<Product>> GetProductsASync(string query_string, string all_words, int page,
            int limit, int description_length);

        Task<PaginatedList<Product>> GetProductsByCategoryAsync(int category_id, int page, int limit,
            int description_length);

        Task<PaginatedList<Product>> GetProductsByDepartmentAsync(int department_id, int page, int limit,
            int description_length);

        Task<ProductDetail> GetProductDetailsAsync(int id);
        Task<IEnumerable<ProductLocations>> GetProductLocationsAsync(int product_id);
        Task<IEnumerable<CustomerReview>> GetProductReviewsAsync(int product_id);
        Task PostProductReviewAsync(string customerEmail, int product_id, string review, short rating);
    }
}