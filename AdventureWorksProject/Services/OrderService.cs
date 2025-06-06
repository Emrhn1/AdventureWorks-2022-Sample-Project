using AdventureWorksProject.Data;
using AdventureWorksProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksProject.Services
{
    public class OrderService
    {
        private readonly AdventureWorksContext _context;

        public OrderService()
        {
            _context = new AdventureWorksContext();
        }

        public async Task<List<SalesOrderHeader>> GetOrderDetailsAsync()
        {
            
            return await _context.SalesOrderHeaders
                .Include(o => o.SalesOrderDetails)
                    .ThenInclude(d => d.Product)
                .ToListAsync();
        }

        public async Task<SalesOrderHeader> CreateOrderAsync(int customerId, List<(int productId, short quantity)> orderItems)
        {
            try
            {
                var now = DateTime.Now;
                var order = new SalesOrderHeader
                {
                    CustomerID = customerId,
                    OrderDate = now,
                    DueDate = now.AddDays(1),
                    BillToAddressID = 1,
                    ShipToAddressID = 1, 
                    ShipMethodID = 1 
                };

             
                _context.SalesOrderHeaders.Add(order);
                await _context.SaveChangesAsync();

         
                foreach (var item in orderItems)
                {
                    var product = await _context.Products.FindAsync(item.productId);
                    if (product == null) continue;

                    var detail = new SalesOrderDetail
                    {
                        ProductID = item.productId,
                        OrderQty = item.quantity,
                        UnitPrice = product.ListPrice,
                        SalesOrderID = order.SalesOrderID 
                    };
                    _context.SalesOrderDetails.Add(detail);
                    await _context.SaveChangesAsync(); 
                }

               
                order = await _context.SalesOrderHeaders
                    .Include(o => o.SalesOrderDetails)
                    .FirstOrDefaultAsync(o => o.SalesOrderID == order.SalesOrderID);
                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Inner: " + ex.InnerException.Message);
                throw;
            }
        }

        public async Task<Dictionary<string, decimal>> GetSalesByCategory()
        {
            var result = await _context.SalesOrderDetails
                .Include(d => d.Product)
                    .ThenInclude(p => p.ProductCategory)
                .Where(d => d.Product != null && d.Product.ProductCategory != null)
                .GroupBy(d => d.Product!.ProductCategory!.Name)
                .Select(g => new { Category = g.Key, Total = g.Sum(d => d.LineTotal) })
                .ToDictionaryAsync(x => x.Category, x => x.Total);
            
            return result;
        }
    }
}
