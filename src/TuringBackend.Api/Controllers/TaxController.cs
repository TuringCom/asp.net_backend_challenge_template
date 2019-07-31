using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TuringBackend.Models.Data;
using TuringBackend.Api.Services;
using TuringBackend.Models;

namespace TuringBackend.Api.Controllers
{
    /// <summary>
    ///     Everything about Tax
    /// </summary>
    [Route("tax")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _taxService;

        public TaxController(ITaxService _taxService, 
            IMapper mapper)
        {
            this._taxService = _taxService;
        }

        /// <summary>
        ///     Get Tax
        /// </summary>
        /// <returns>Return a list of tax.</returns>
        /// <response code="200">An array of object Tax</response>
        /// <response code="400">Return a error object</response>
        [HttpGet]
        [ProducesResponseType(typeof(Tax), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        /// <summary>
        ///     Get Tax by ID
        /// </summary>
        /// <param name="tax_id"></param>
        /// <returns>Return a tax by ID.</returns>
        [HttpGet("{tax_id:int}")]
        [ProducesResponseType(typeof(Tax), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Get(int tax_id)
        {
            //TODO: Complete the code here
            return Ok();
        }
    }
}