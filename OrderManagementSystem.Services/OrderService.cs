using AutoMapper;
using OrderManagementSystem.Core.DataTransferObjects;
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
        private readonly IMapper _mapper;
        private readonly ICustomerRepo _customerRepo;
        private readonly IInvoiceRepo _invoiceRepo;
        private readonly IEmailService _emailService;


        public OrderService(IOrderRepo orderRepository, IProductRepo productRepository, IInvoiceService invoiceService, IMapper mapper, ICustomerRepo customerRepo, IInvoiceRepo invoiceRepo, IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _invoiceService = invoiceService;
            _mapper = mapper;
            _customerRepo = customerRepo;
            _invoiceRepo = invoiceRepo;
            _emailService = emailService;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
           var order= await _orderRepository.GetAllOrdersAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(order);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
        {
            var order= await _orderRepository.GetOrderByIdAsync(orderId);
        
            return _mapper.Map<OrderDto>(order);
        
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
            var customer= await _customerRepo.GetCustomerByIdAsync(order.CustomerId);
            // Send Email Logic

            var subject = $"Order #{orderId} Status Update";
            var message = $"Your order #{orderId} status has been updated to {status}.";
            await _emailService.SendEmailAsync(customer.Email, subject, message);
        }

        /// 
        ////

        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            var order = new Order
            {
                CustomerId = orderCreateDto.CustomerId,
                OrderDate = DateTime.UtcNow,
                PaymentMethod = orderCreateDto.PaymentMethod,
                Status =  "Pending" ,
                OrderItems = new List<OrderItem>()
            };

            decimal totalAmount = 0;

            foreach (var item in orderCreateDto.OrderItems)
            {
                var customer = await _customerRepo.GetCustomerByIdAsync(orderCreateDto.CustomerId);
                if (customer == null)
                {
                    throw new Exception($"Customer with ID {orderCreateDto.CustomerId} does not exist.");
                }
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product == null || product.Stock < item.Quantity)
                {
                    throw new Exception($"Product with ID {item.ProductId} not available or insufficient stock.");
                }

                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    Discount = item.Discount
                };

                totalAmount += (orderItem.UnitPrice - orderItem.Discount) * orderItem.Quantity;
                order.OrderItems.Add(orderItem);

               
                product.Stock -= item.Quantity;
            }

            if (totalAmount > 200)
            {
                totalAmount *= 0.90m; 
            }
            else if (totalAmount > 100)
            {
                totalAmount *= 0.95m; 
            }

            order.TotalAmount = totalAmount;

            await _orderRepository.AddOrderAsync(order);

            await _orderRepository.SaveChangesAsync();
            await _invoiceService.GenerateInvoiceAsync(order.OrderId);
           await _invoiceRepo.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

      

     





    }
}
