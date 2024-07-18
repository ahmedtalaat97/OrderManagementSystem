using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Errors;
using OrderManagementSystem.Core.DataTransferObjects;
using OrderManagementSystem.Core.Entites;
using OrderManagementSystem.Core.Services;

namespace OrderManagementSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Invoices : ControllerBase
    {

        private IInvoiceService _invoiceService;

        public Invoices(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }



        [HttpGet]

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetInvoicesAsync();
            if (invoices == null)
            {
                return NotFound(new ApiResponse(404 ,$"No Invoice founded "));
            }
            return Ok(invoices);
        }



        [HttpGet("{invoiceId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<InvoiceDto>> GetInvoiceById(int invoiceId)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId);
            if (invoice == null)
            {
                return NotFound(new ApiResponse(404,$"no invoices with id {invoiceId} founded"));
            }
            return Ok(invoice);
        }

    }
}
