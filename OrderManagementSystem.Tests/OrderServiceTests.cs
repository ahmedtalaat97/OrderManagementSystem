using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OrderManagementSystem.APIs.Helper;
using OrderManagementSystem.Core.DataTransferObjects;
using OrderManagementSystem.Core.Entites;
using OrderManagementSystem.Core.Repositories;
using OrderManagementSystem.Core.Services;
using OrderManagementSystem.Repository;
using OrderManagementSystem.Repository.Data;
using OrderManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrderManagementSystem.Tests
{
    public class OrderServiceTests
    {

        private readonly DbContextOptions<OrderContext> _dbContextOptions;

        public OrderServiceTests()
        {
            // Initialize DbContext options for in-memory database
            _dbContextOptions = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(databaseName: "Test_OrderManagementDB")
                .Options;

            // Seed the in-memory database with test data
            SeedTestData();
        }

        private void SeedTestData()
        {
            using (var context = new OrderContext(_dbContextOptions))
            {
                // Seed test data (customers, products, etc.) as needed for your tests
                var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, Name = "Customer 1", Email = "customer1@example.com" },
                new Customer { CustomerId = 2, Name = "Customer 2", Email = "customer2@example.com" }
            };

                context.Customers.AddRange(customers);

                var products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product 1", Price = 10, Stock = 100 },
                new Product { ProductId = 2, Name = "Product 2", Price = 20, Stock = 50 }
            };

                context.Products.AddRange(products);

                context.SaveChanges();
            }
        }

        [Fact]
        public async void PlaceOrderAsync_Should_UpdateProductStock()
        {
            // Arrange
            using (var context = new OrderContext(_dbContextOptions))
            {
                var orderRepository = new OrderRepo(context);
                var productRepository = new ProductRepo(context);
                var invoiceService = new Mock<IInvoiceService>().Object;
                var mapper = new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile());
                }));
                var customerRepository = new CustomerRepo(context);
                var invoiceRepository = new InvoiceRepo(context);
                var emailService = new Mock<IEmailService>().Object;

                var orderService = new OrderService(orderRepository, productRepository, invoiceService, mapper, customerRepository, invoiceRepository, emailService);

                var orderCreateDto = new OrderCreateDto
                {
                    CustomerId = 1,
                    OrderItems = new List<OrderItemCreateDto>
                {
                    new OrderItemCreateDto { ProductId = 1, Quantity = 5 }
                },
                    PaymentMethod = "Credit Card"
                };

                // Act
                var orderId = await orderService.CreateOrderAsync(orderCreateDto);

                // Assert
                var order = await context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId.OrderId);

                Assert.NotNull(order);
                Assert.Equal(1, order.OrderItems.Count()); 
                Assert.Equal(95, order.OrderItems.First().Product.Stock); 

            }


        }
    }
}
