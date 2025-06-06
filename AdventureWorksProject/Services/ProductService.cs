using AdventureWorksProject.Data;
using AdventureWorksProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AdventureWorksProject.Services
{
    public class ProductService
    {
        private readonly AdventureWorksContext _context;

        public ProductService()
        {
            _context = new AdventureWorksContext();
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int? categoryId)
        {
            IQueryable<Product> query = _context.Products;
            query = query.Include(p => p.ProductCategory);
            
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.ProductSubcategoryID == categoryId);
            }

            return await query.ToListAsync();
        }

        public async Task<List<ProductCategory>> GetCategoriesAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }
    }
}
