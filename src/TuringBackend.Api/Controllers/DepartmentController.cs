using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TuringBackend.Models.Data;
using TuringBackend.Api.Services;
using TuringBackend.Models;

namespace TuringBackend.Api.Controllers
{
    /// <summary>
    ///     Everything about Department
    /// </summary>
    [Route("departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get Departments
        /// </summary>
        /// <returns>Return a list of departments.</returns>
        /// <response code="200">An array of object Department</response>
        /// <response code="200">Return a error object</response>
        [HttpGet]
        [ProducesResponseType(typeof(Department), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        /// <summary>
        ///     Get Department by ID
        /// </summary>
        /// <param name="department_id"></param>
        /// <returns>Return a department by ID.</returns>
        [HttpGet("{department_id:int}")]
        [ProducesResponseType(typeof(Department), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Get(int department_id)
        {
           return Ok();
        }
    }
}