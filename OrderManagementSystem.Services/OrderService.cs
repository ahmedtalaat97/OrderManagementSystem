using OrderManagementSystem.Core.Entites;
using OrderManagementSystem.Core.Repositories;
using OrderManagementSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepository;
        private readonly IProductRepo _productRepository;
        private readonly IInvoiceService _invoiceService;

        public OrderService(IOrderRepo orderRepository, IProductRepo productRepository, IInvoiceService invoiceService)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _invoiceService = invoiceService;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _orderRepository.GetOrdersAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task PlaceOrderAsync(Order order)
        {
            // Validate order, apply discounts, update stock, etc.
            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product.Stock < item.Quantity)
                {
                    throw new InvalidOperationException("Insufficient stock for product: " + product.Name);
                }
                product.Stock -= item.Quantity;
            }


            if (order.TotalAmount > 200)
            {
                order.TotalAmount = 0.9m;
            }
            else if (order.TotalAmount > 100)
            {
                order.TotalAmount = 0.95m;
            }

            await _orderRepository.AddOrderAsync(order);
            await _orderRepository.SaveChangesAsync();

            await _invoiceService.GenerateInvoiceAsync(order.OrderId);
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found");
            }

            order.Status = status;
            await _orderRepository.UpdateOrderAsync(order);
            await _orderRepository.SaveChangesAsync();

            // Send Email Logic
        }

      

    }
}
