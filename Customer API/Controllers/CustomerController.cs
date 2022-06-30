using AutoMapper;
using Customer_API.DTOs;
using Customer_API.Helpers;
using Customer_API.Model;
using Customer_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Customer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerController(IMapper mapper, ICustomerService _customerService)
        {
            this._mapper = mapper;
            this._customerService = _customerService;
        }

        /// <summary>
        /// Returns a list of customers.
        /// </summary>
        /// <returns></returns>
        // GET: api/<CustomerController>
        [HttpGet("List")]
        public async Task<IActionResult> GetCustomers([FromQuery] CustomerParams customerParams)
        {
            var customers = await _customerService.GetAll(customerParams);
            var customerDto = new List<CustomerDto>();

            foreach (var customer in customers)
                customerDto.Add(_mapper.Map<CustomerDto>(customer));

            Response.AddPagination(customers.CurrentPage, customers.PageSize, customers.TotalCount, customers.TotalPages);
            return Ok(customerDto);
        }

        /// <summary>
        /// Get a customer record.
        /// </summary>
        /// <param name="id"> The Id of customer. </param>
        /// <returns></returns>
        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await this._customerService.Get(id);
            if (customer == null)
                return NotFound();

            var customerDto = _mapper.Map<CustomerDto>(customer);

            return Ok(customerDto);
        }

        /// <summary>
        /// Creates a customer
        /// </summary>
        /// <param name="customerDto">customer input object</param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
                return BadRequest(ModelState);

            var customer = _mapper.Map<Customer>(customerDto);

            if (!await _customerService.Create(customer))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record { customer.FirstName }");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }


        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <param name="id">customer id.</param>
        /// <param name="customerDto">customer object.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CustomerDto customerDto)
        {
            if (customerDto == null || id != customerDto.Id)
                return BadRequest(ModelState);

            var customer = _mapper.Map<Customer>(customerDto);

            if (!await _customerService.Exists(id))
                return NotFound();

            if (await _customerService.Update(customer))
                return NoContent();

            throw new Exception($"Updating customer {id} failed on save");
        }

        /// <summary>
        /// Removes customer.
        /// </summary>
        /// <param name="id">customer id.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _customerService.Exists(id))
                return NotFound();

            if (!await this._customerService.Delete(id))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record { id }");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }
    }
}
