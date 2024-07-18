using OrderManagementSystem.Core.DataTransferObjects;
using OrderManagementSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> GenerateInvoiceAsync(int orderId);
        Task<InvoiceDto> GetInvoiceByIdAsync(int invoiceId);
        Task<IEnumerable<InvoiceDto>> GetInvoicesAsync();
    }
}
