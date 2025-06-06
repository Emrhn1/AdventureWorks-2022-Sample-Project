using Microsoft.EntityFrameworkCore;
using AdventureWorksProject.Models;

namespace AdventureWorksProject.Data
{
    public class AdventureWorksContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=LAPTOP-MCMLKEN2\\SQLEXPRESSEMIR;Initial Catalog=AdventureWorks2022;User ID=sa;Password=123;TrustServerCertificate=True;Integrated Security=False;Connection Timeout=30;MultipleActiveResultSets=true;App=EntityFramework;");
        }
    }
}
