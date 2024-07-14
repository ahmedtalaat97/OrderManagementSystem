using OrderManagementSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Repositories
{
    public interface IProductRepo
    {
            Task<IEnumerable<Product>> GetProductsAsync();
            Task<Product> GetProductByIdAsync(int productId);
            Task AddProductAsync(Product product);
            Task UpdateProductAsync(Product product);
            Task SaveChangesAsync();
       
    }
}
