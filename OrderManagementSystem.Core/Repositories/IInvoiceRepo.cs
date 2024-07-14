using OrderManagementSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Repositories
{
    public interface IInvoiceRepo
    {
        Task<IEnumerable<Invoice>> GetInvoicesAsync();
        Task<Invoice> GetInvoiceByIdAsync(int invoiceId);
        Task AddInvoiceAsync(Invoice invoice);
        Task SaveChangesAsync();
    }
}
