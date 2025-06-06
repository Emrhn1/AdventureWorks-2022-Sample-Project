using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksProject.Models
{
    [Table("Product", Schema = "Production")]
    public class Product
    {
        [Key]
        [Column("ProductID")]
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProductNumber { get; set; } = string.Empty;
        public decimal ListPrice { get; set; }
        [Column("ProductSubcategoryID")]
        public int? ProductSubcategoryID { get; set; }
        [ForeignKey("ProductSubcategoryID")]
        public ProductCategory? ProductCategory { get; set; }
    }
}
