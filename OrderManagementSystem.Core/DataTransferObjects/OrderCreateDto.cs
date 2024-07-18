using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.DataTransferObjects
{
    public class OrderCreateDto
    {
        public int CustomerId { get; set; }
        public List<OrderItemCreateDto> OrderItems { get; set; }
        public string PaymentMethod { get; set; }
    }
}
