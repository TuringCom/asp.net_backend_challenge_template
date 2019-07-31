using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuringBackend.Api.Core;
using TuringBackend.Models;
using TuringBackend.Models.Data;

namespace TuringBackend.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly TuringBackendContext _dbContext;
        private readonly ICustomerService _customerService;

        public ProductService(TuringBackendContext dbContext, ICustomerService customerService)
        {
            _dbContext = dbContext;
            _customerService = customerService;
        }

        public async Task<PaginatedList<Product>> GetProductsASync(int page = 1, int limit = 20, string sortColumn = null)
        {
            return await _dbContext
                .Product
                .ToPaginatedListAsync(page, limit, sortColumn);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _dbContext
                .Product
                .FirstOrDefaultAsync(d => d.ProductId == id);
            return product;
        }

        public async Task<PaginatedList<Product>> GetProductsASync(string query_string, string all_words, int page,
            int limit, int description_length)
        {
            return await _dbContext
                .Product
                .ToPaginatedListAsync(page, limit, null);
        }

        public async Task<PaginatedList<Product>> GetProductsByCategoryAsync(int category_id, int page, int limit,
            int description_length)
        {
            // TODO: include other parameters
            return await _dbContext
                .Product
                .ToPaginatedListAsync(page, limit, null);
        }

        public async Task<PaginatedList<Product>> GetProductsByDepartmentAsync(int department_id, int page, int limit,
            int description_length)
        {
            return await _dbContext
                .Product
                .ToPaginatedListAsync(page, limit, null);
        }

        public async Task<ProductDetail> GetProductDetailsAsync(int id)
        {
            var product = await _dbContext.Product
                .Select(p => new ProductDetail
                {
                    ProductId = p.ProductId,
                    Price = p.Price,
                    Description = p.Description,
                    DiscountedPrice = p.DiscountedPrice,
                    Name = p.Name,
                    Image = p.Image,
                    Image2 = p.Image2
                })
                .FirstOrDefaultAsync(d => d.ProductId == id);
            return product;
        }

        public async Task<IEnumerable<ProductLocations>> GetProductLocationsAsync(int product_id)
        {
            var categories = from pc in _dbContext.ProductCategory
                join c in _dbContext.Category on pc.CategoryId equals c.CategoryId
                join d in _dbContext.Department on c.DepartmentId equals d.DepartmentId
                where pc.ProductId == product_id
                select new ProductLocations
                {
                    CategoryId = c.CategoryId,
                    DepartmentId = c.DepartmentId,
                    CategoryName = c.Name,
                    DepartmentName = d.Name
                };
            return await categories.ToListAsync();
        }

        public async Task<IEnumerable<CustomerReview>> GetProductReviewsAsync(int product_id)
        {
            var categories = from pc in _dbContext.Review
                join c in _dbContext.Customer on pc.CustomerId equals c.CustomerId
                where pc.ProductId == product_id
                select new CustomerReview
                {
                    Name = c.Name,
                    Rating = pc.Rating,
                    Review = pc.Review1,
                    CreatedOn = pc.CreatedOn.ToString(CultureInfo.InvariantCulture)
                };
            return await categories.ToListAsync();
        }


        public async Task PostProductReviewAsync(string customerEmail, int product_id, string review, short rating)
        {
            var loggedOnCustomer = await _customerService.GetCustomerByEmailAsync(customerEmail);
            var productReview = new Review
            {
                CustomerId = loggedOnCustomer.CustomerId,
                ProductId = product_id,
                Review1 = review,
                Rating = rating,
                CreatedOn = DateTime.Now
            };
            _dbContext.Review.Add(productReview);
            await _dbContext.SaveChangesAsync();
        }
    }
}