using OrderManagementSystem.Core.DataTransferObjects;
using OrderManagementSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<Product> AddProductAsync(ProductCreateDto product);
        Task<ProductDto> UpdateProductAsync(ProductDto product);
    }
}
