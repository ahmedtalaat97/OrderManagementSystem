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
        Task GenerateInvoiceAsync(int orderId);
        Task<Invoice> GetInvoiceByIdAsync(int invoiceId);
        Task<IEnumerable<Invoice>> GetInvoicesAsync();
    }
}
