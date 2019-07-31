using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuringBackend.Models.Data;
using TuringBackend.Api.Core;
using TuringBackend.Api.Services;
using TuringBackend.Models;

namespace TuringBackend.Api.Controllers
{
    /// <summary>
    ///     Everything about Product
    /// </summary>
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IAuthenticationService authenticationService, IMapper mapper)
        {
            _productService = productService;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get Products
        /// </summary>
        /// <returns>Return a list of products.</returns>
        /// <response code="200">An array of object Product</response>
        /// <response code="200">Return a error object</response>
        [HttpGet]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Get(string order, int page = 1, int limit = 20)
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Get Product by ID
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns>Return a product by ID.</returns>
        [HttpGet("{product_id:int}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Get(int product_id)
        {
            var product = await _productService.GetProductByIdAsync(product_id);
            return Ok(product);
        }

        /// <summary>
        ///     Search Products
        /// </summary>
        /// <param name="query_string"></param>
        /// <param name="all_words"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="description_length"></param>
        /// <returns>Search Products.</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Search(string query_string, string all_words = "on", int page = 1,
            int limit = 20, int description_length = 200)
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Search Products of Categories
        /// </summary>
        /// <param name="category_id"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="description_length"></param>
        /// <returns>Search Products of Categories.</returns>
        [HttpGet("inCategory/{category_id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> ProductsByCategory(int category_id, int page = 1, int limit = 20,
            int description_length = 200)
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Search Products of Categories
        /// </summary>
        /// <param name="department_id"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="description_length"></param>
        /// <returns>Search Products on Department.</returns>
        [HttpGet("inDepartment/{department_id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> ProductsByDepartment(int department_id, int page = 1, int limit = 20,
            int description_length = 200)
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Get details of a Product
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns>Get details of a Product.</returns>
        [HttpGet("{product_id:int}/details")]
        [ProducesResponseType(typeof(ProductDetail), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetProductDetails(int product_id)
        {
            //TODO: Complete the code here;
            return Ok();
        }

        /// <summary>
        ///     Get locations of a
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns>Get locations of a Product.</returns>
        [HttpGet("{product_id:int}/locations")]
        [ProducesResponseType(typeof(ProductLocations), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetProductLocations(int product_id)
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Get reviews of a Product
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns>Get reviews of a Product.</returns>
        [HttpGet("{product_id:int}/reviews")]
        [ProducesResponseType(typeof(CustomerReview), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetProductReviews(int product_id)
        {
            var category = await _productService.GetProductReviewsAsync(product_id);
            return Ok(category);
        }

        /// <summary>
        ///     Get reviews of a Product
        /// </summary>
        /// <param name="product_id">ProductID</param>
        /// <param name="review">Review Text of the Product</param>
        /// <param name="rating">Rating of Product</param>
        /// <returns>Get reviews of a Product.</returns>
        /// <response code="200">No data.</response>
        /// <response code="400">Return a error object</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost("{product_id:int}/reviews")]
        [Authorize]
        [ProducesResponseType(typeof(CustomerReview), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        public async Task<IActionResult> PostProductReviews(
            int product_id,
            [FromForm] [Required] string review,
            [FromForm] [Required] short rating)
        {
            //TODO: Complete the code here;
            return Ok();
        }
    }
}