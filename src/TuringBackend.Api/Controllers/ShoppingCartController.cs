using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TuringBackend.Models.Data;
using TuringBackend.Api.Core;
using TuringBackend.Api.Services;
using TuringBackend.Models;

namespace TuringBackend.Api.Controllers
{
    /// <summary>
    ///     Everything about ShoppingCart
    /// </summary>
    [Route("shoppingcart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ShoppingCartController(IShoppingCartService shoppingCartService, 
            IAuthenticationService authenticationService, 
            IProductService productService, 
            IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _authenticationService = authenticationService;
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Generate the unique CART ID
        /// </summary>
        /// <returns>Return a list of shoppingcart.</returns>
        /// <response code="200">An array of object ShoppingCart</response>
        /// <response code="400">Return a error object</response>
        [HttpGet("generateUniqueId")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GenerateUniqueId()
        {
            var cart = new
            {
                cart_id = Guid
                    .NewGuid()
                    .ToString()
                    .Replace("-", "")
                    .Substring(0, 15)
            };
            return Ok(await Task.FromResult(cart));
        }

        /// <summary>
        ///     Add a Product in Shopping Cart
        /// </summary>
        /// <param name="cart_id">Cart ID</param>
        /// <param name="product_id">Product ID</param>
        /// <param name="attributes">Attributes of a product</param>
        /// <returns>Return a cart by ID.</returns>
        [HttpPost("add")]
        [ProducesResponseType(typeof(ShoppingCart), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Add(
            [Required] [FromForm] string cart_id,
            [Required] [FromForm] string product_id,
            [Required] [FromForm] string attributes
        )
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Get List of Products in Shopping Cart
        /// </summary>
        /// <param name="cart_id">Cart ID</param>
        /// <returns>Return a cart by ID.</returns>
        [HttpGet("{cart_id}")]
        [ProducesResponseType(typeof(CartWithProduct), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Get(string cart_id)
        {
            var cartItems = await _shoppingCartService.GetShoppingCartByIdAsync(cart_id);
            return Ok(cartItems);
        }

        /// <summary>
        ///     Update Shopping Cart item
        /// </summary>
        /// <param name="item_id">Item ID</param>
        /// <param name="quantity">Item Quantity</param>
        /// <response code="200">Return a array of products in the cart.</response>
        /// <response code="400">Return a error object</response>
        [HttpPut("update/{item_id}")]
        [ProducesResponseType(typeof(ShoppingCart), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> UpdateCartItem(
            [Required] int item_id,
            [Required] [FromForm] int quantity
        )
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Empty cart
        /// </summary>
        /// <param name="cart_id">Cart ID</param>
        /// <response code="200">Return an empty array.</response>
        /// <response code="400">Return a error object</response>
        [HttpDelete("empty/{cart_id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> EmptyCart(
            [Required] string cart_id
        )
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Move a product to cart
        /// </summary>
        /// <param name="item_id">Item ID</param>
        [HttpGet("moveToCart/{item_id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> MoveToCart(
            [Required] int item_id
        )
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Return a total Amount from Cart
        /// </summary>
        /// <param name="cart_id">Cart ID</param>
        [HttpGet("totalAmount/{cart_id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> TotalAmount(
            [Required] string cart_id
        )
        {
            var amount = await _shoppingCartService.GetCartTotalAmountAsync(cart_id);
            return Ok(new
            {
                total_amount = amount
            });
        }

        /// <summary>
        ///     Save a Product for later
        /// </summary>
        /// <param name="item_id">Item ID</param>
        [HttpGet("saveForLater/{item_id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> SaveForLater(
            [Required] int item_id
        )
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Get Products saved for later
        /// </summary>
        /// <param name="cart_id">Item ID</param>
        [HttpGet("getSaved/{cart_id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetSaved(
            [Required] string cart_id
        )
        {
            var savedItems = await _shoppingCartService.GetSavedCartItemsAsync(cart_id);
            return Ok(savedItems);
        }

        /// <summary>
        ///     Empty cart
        /// </summary>
        /// <param name="item_id">Item ID</param>
        /// <response code="200">No data</response>
        /// <response code="400">Return a error object</response>
        [HttpDelete("removeProduct/{item_id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> RemoveProduct(
            [Required] int item_id
        )
        {
            var cartId = await _shoppingCartService.GetCartIdByItemAsync(item_id);
            if (cartId == null) 
                return BadRequest(new Error(400, "USR_10", $"Item {item_id}' not found in the cart.", nameof(item_id)));

            await _shoppingCartService.RemoveItemAsync(item_id);
            return Ok();
        }
    }
}