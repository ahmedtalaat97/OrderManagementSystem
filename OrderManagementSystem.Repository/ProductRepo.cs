using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Core.Entites;
using OrderManagementSystem.Core.Repositories;
using OrderManagementSystem.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly OrderContext _context;

        public ProductRepo(OrderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

      

    }
}
