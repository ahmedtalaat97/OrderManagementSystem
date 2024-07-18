using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Errors;
using OrderManagementSystem.Core.DataTransferObjects;
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

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(404,"Not a Valid Customer"));
            }

            var createdCustomer = await _customerService.CreateCustomerAsync(customerDto);
            return Ok(createdCustomer);
        }

        [HttpGet("{customerId}/orders")]
        public async Task<ActionResult<OrderDto>> GetCustomerOrders(int customerId)
        {
            var orders = await _customerService.GetCustomerOrdersAsync(customerId);
            if (orders == null|| orders.Count()==0)
            {
                return NotFound(new ApiResponse(404, $"No Customer Orders with id {customerId} founded"));
            }

            return Ok(orders);
        }
    }
}
