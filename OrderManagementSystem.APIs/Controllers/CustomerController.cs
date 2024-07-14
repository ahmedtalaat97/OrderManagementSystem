using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Core.Entites;
using OrderManagementSystem.Core.Services;

namespace OrderManagementSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // POST: api/customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer is null.");
            }

            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { customerId = customer.CustomerId }, customer);
        }

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomersAsync();
            return Ok(customers);
        }

        // GET: api/customers/{customerId}
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // GET: api/customers/{customerId}/orders
        [Authorize(Roles = "Customer, Admin")]
        [HttpGet("{customerId}/orders")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            var orders = await _customerService.GetCustomerOrdersAsync(customerId);
            return Ok(orders);
        }
    }
}
