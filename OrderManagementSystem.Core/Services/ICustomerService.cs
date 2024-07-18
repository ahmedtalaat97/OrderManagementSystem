using OrderManagementSystem.Core.DataTransferObjects;
using OrderManagementSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomerAsync(CustomerDto customerDto);
        Task<List<OrderDto>> GetCustomerOrdersAsync(int customerId);
    }
}
