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
    ///     Everything about Order
    /// </summary>
    [Authorize]
    [Route("orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomerService _customerService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, 
            IAuthenticationService authenticationService, 
            ICustomerService customerService,
            IEmailService emailService, 
            IMapper mapper)
        {
            _orderService = orderService;
            _authenticationService = authenticationService;
            _customerService = customerService;
            _emailService = emailService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get Info about Order
        /// </summary>
        /// <param name="cart_id"></param>
        /// <param name="shipping_id"></param>
        /// <param name="tax_id"></param>
        /// <returns>Return a order by ID.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Orders), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        public async Task<IActionResult> Post(
            [Required] [FromForm] string cart_id,
            [Required] [FromForm] int shipping_id,
            [Required] [FromForm] int tax_id
        )
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Get Info about Order
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>Return a order by ID.</returns>
        [HttpGet("{order_id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(Order), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Get(int order_id)
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Get orders by Customer
        /// </summary>
        /// <returns>Return a list of order.</returns>
        /// <response code="200">An array of object Order</response>
        /// <response code="400">Return a error object</response>
        [HttpGet("inCustomer")]
        [ProducesResponseType(typeof(CartWithProduct), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetOrdersByCustomer()
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Get Info about Order
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>Return a order by ID.</returns>
        [HttpGet("/orders/shortDetail/{order_id:int}")]
        [ProducesResponseType(typeof(OrderDetail), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetOrderDetail(int order_id)
        {
            //TODO: Complete the code here
            return Ok();
        }

        private async Task<int> GetCustomerIdAsync()
        {
            var email = _authenticationService.GetUserId(User);
            var customer =  await _customerService.GetCustomerByEmailAsync(email);
            return customer.CustomerId;
        }
    }
}