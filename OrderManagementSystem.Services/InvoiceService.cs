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
    public class InvoiceService:IInvoiceService
    {
        private readonly IInvoiceRepo _invoiceRepository;
        private readonly IOrderRepo _orderRepository;

        public InvoiceService(IInvoiceRepo invoiceRepository, IOrderRepo orderRepository)
        {
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
        }

        public async Task GenerateInvoiceAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found");
            }

            var invoice = new Invoice
            {
                OrderId = orderId,
                InvoiceDate = DateTime.Now,
                TotalAmount = order.TotalAmount
            };

            await _invoiceRepository.AddInvoiceAsync(invoice);
            await _invoiceRepository.SaveChangesAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int invoiceId)
        {
            return await _invoiceRepository.GetInvoiceByIdAsync(invoiceId);
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesAsync()
        {
            return await _invoiceRepository.GetInvoicesAsync();
        }
    }
}
