using OrderManagementSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services
{
    public interface IOrderService
    {
            Task<IEnumerable<Order>> GetOrdersAsync();
            Task<Order> GetOrderByIdAsync(int orderId);
            Task PlaceOrderAsync(Order order);
            Task UpdateOrderStatusAsync(int orderId, string status);
        
    }
}
