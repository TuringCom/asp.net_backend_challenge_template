using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using TuringBackend.Models.Data;
using TuringBackend.Api.Core;
using TuringBackend.Api.Services;
using TuringBackend.Models;

namespace TuringBackend.Api.Controllers
{
    /// <summary>
    ///     Everything about Customer
    /// </summary>
    [Route("customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICreditCardService _creditCardService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, 
            IAuthenticationService authenticationService,
            ICreditCardService creditCardService,
            IMapper mapper)
        {
            _authenticationService = authenticationService;
            _creditCardService = creditCardService;
            _mapper = mapper;
            _customerService = customerService;
        }

        /// <summary>
        ///     Update a customer
        /// </summary>
        /// <returns>Return a Object of Customer with auth credentials.</returns>
        /// <param name="name">Customer name</param>
        /// <param name="email">Customer email</param>
        /// <param name="password">Customer password</param>
        /// <param name="day_phone">Customer day phone</param>
        /// <param name="eve_phone">Customer eve phone</param>
        /// <param name="mob_phone">Customer mob phone</param>
        /// <response code="200">An array of object Category</response>
        /// <response code="400">Return a error object</response>
        /// <response code="401">Unauthorized</response>
        [HttpPut("~/customer")]
        [Authorize]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Update(
            [Required] [FromForm] string name,
            [Required] [FromForm] string email,
            string password,
            string day_phone,
            string eve_phone,
            string mob_phone
        )
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Get Customer by ID
        /// </summary>
        /// <returns>Return a Object of Customer with auth credentials.</returns>
        /// <response code="200">A customer</response>
        /// <response code="400">Return a error object</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet("~/customer")]
        [Authorize]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(401)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Get()
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Register a Customer
        /// </summary>
        /// <param name="name">Customer name</param>
        /// <param name="email">Email of User.</param>
        /// <param name="password">Password of User</param>
        /// <response code="200">Return a Object of Customer with auth credentials</response>
        /// <response code="400">Return a error object</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Post(
            [Required] [FromForm] string name,
            [Required] [FromForm] string email,
            [Required] [FromForm] string password
        )
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Sign in in the Shopping.
        /// </summary>
        /// <param name="email">Email of User</param>
        /// <param name="password">Password of User</param>
        /// <response code="200">Return a Object of Customer with auth credentials</response>
        /// <response code="400">Return a error object</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CustomerRegister), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Login(
            [Required] [FromForm] string email,
            [Required] [FromForm] string password
        )
        {
            // Validate email
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Sign in with a facebook login token.
        /// </summary>
        /// <returns>Token generated from your facebook login</returns>
        /// <response code="200">Return a Object of Customer with auth credentials</response>
        /// <response code="400">Return a error object</response>
        [HttpPost("facebook")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> FaceBookLogin(
            [Required] [FromForm] string access_token
        )
        {
            await Task.CompletedTask;
            return Ok();
        }

        /// <response code="200">Return a Object of Customer with auth credentials</response>
        /// <response code="400">Return a error object</response>
        [HttpPut("address")]
        [Authorize]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> UpdateAddress(
            [Required] [FromForm] string Address_1,
                       [FromForm] string address_2,
            [Required] [FromForm] string city,
            [Required] [FromForm] string region,
            [Required] [FromForm] string postal_code,
            [Required] [FromForm] string country,
            [Required] [FromForm] int shipping_region_id
        )
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <response code="200">Return a Object of Customer with auth credentials</response>
        /// <response code="400">Return a error object</response>
        [HttpPut("creditCard")]
        [Authorize]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> UpdateCreditCard(
            [Required] [FromForm] string credit_card
        )
        {
            //TODO: Complete the code here
            return Ok();
        }

    }
}