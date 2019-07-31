using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TuringBackend.Models.Data;
using TuringBackend.Api.Services;
using TuringBackend.Models;

namespace TuringBackend.Api.Controllers
{
    /// <summary>
    ///     Everything about Shipping
    /// </summary>
    [Route("shipping")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingService _shippingService;
        private readonly IMapper _mapper;

        public ShippingController(IShippingService shippingService, IMapper mapper)
        {
            _shippingService = shippingService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get Shipping
        /// </summary>
        /// <returns>Return a list of shipping.</returns>
        /// <response code="200">An array of object Shipping</response>
        /// <response code="200">Return a error object</response>
        [HttpGet]
        [ProducesResponseType(typeof(Shipping), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _shippingService.GetShippingAsync());
        }

        /// <summary>
        ///     Get Shipping by ID
        /// </summary>
        /// <param name="shipping_id"></param>
        /// <returns>Return a shipping by ID.</returns>
        [HttpGet("{shipping_id:int}")]
        [ProducesResponseType(typeof(Shipping), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Get(int shipping_id)
        {
            var shipping = await _shippingService.GetShippingByIdAsync(shipping_id);
            return Ok(shipping);
        }
    }
}