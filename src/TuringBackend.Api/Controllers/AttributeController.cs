using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TuringBackend.Models.Data;
using TuringBackend.Api.Services;
using TuringBackend.Models;

namespace TuringBackend.Api.Controllers
{
    /// <summary>
    ///     Everything about Attribute
    /// </summary>
    [Route("attributes")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeService _attributeService;
        private readonly IMapper _mapper;

        public AttributeController(IAttributeService attributeService, IMapper mapper)
        {
            _attributeService = attributeService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get Attributes
        /// </summary>
        /// <returns>Return a list of attributes.</returns>
        /// <response code="200">An array of object Attribute</response>
        /// <response code="200">Return a error object</response>
        [HttpGet]
        [ProducesResponseType(typeof(Attribute), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Get()
        {
            //TODO: Complete the code here
            return Ok();
        }

        /// <summary>
        ///     Get Attribute by ID
        /// </summary>
        /// <param name="attribute_id"></param>
        /// <returns>Return a attribute by ID.</returns>
        [HttpGet("{attribute_id:int}")]
        [ProducesResponseType(typeof(Attribute), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Get(int attribute_id)
        {
            var attribute = await _attributeService.GetAttributeByIdAsync(attribute_id);
            
            return Ok(attribute);
        }

        /// <summary>
        ///     Get Values Attribute from Atribute
        /// </summary>
        /// <param name="attribute_id"></param>
        /// <returns>Get Values Attribute from Attribute.</returns>
        [HttpGet("values/{attribute_id:int}")]
        [ProducesResponseType(typeof(Attribute), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetAttributeValueByAttributeId(int attribute_id)
        {
            // TODO: use mapping,
            // var attribute = _mapper.Map<AttributeValueByAttribute>();
            // TODO: Complete the code here
            return Ok();
        }


        /// <summary>
        ///     Get all Attributes with Product ID
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns>Get all Attributes with Product ID</returns>
        [HttpGet("inProduct/{product_id:int}")]
        [ProducesResponseType(typeof(Attribute), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetAttributeValueByProductId(int product_id)
        {
            var attribute = await _attributeService.GetAttributeValueByProductIdAsync(product_id);
            return Ok(attribute);
        }
    }
}