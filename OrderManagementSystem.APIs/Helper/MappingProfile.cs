using AutoMapper;
using OrderManagementSystem.Core.DataTransferObjects;
using OrderManagementSystem.Core.Entites;

namespace OrderManagementSystem.APIs.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer,CustomerDto >().ReverseMap();
            
      
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();


            CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => CalculateOrderTotal(src.OrderItems)));

            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItemCreateDto, OrderItem>();
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<Invoice, InvoiceDto>().ReverseMap();
        }
        private decimal CalculateOrderTotal(List<OrderItem> orderItems)
        {


            var orders= orderItems.Sum(item => (item.Quantity * item.UnitPrice) - item.Discount);

             if (orders>200)
            {
                orders = orders * 0.90m;
            }
            else if (orders>100)
            {
                orders = orders * 0.95m;

            }
            return orders;
        }

    }
}
