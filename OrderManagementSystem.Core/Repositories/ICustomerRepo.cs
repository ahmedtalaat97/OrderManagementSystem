using OrderManagementSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Repositories
{
    public interface ICustomerRepo
    {
            Task<IEnumerable<Customer>> GetCustomersAsync();
            Task<Customer> GetCustomerByIdAsync(int customerId);
            Task AddCustomerAsync(Customer customer);
            Task SaveChangesAsync();
    }
}
