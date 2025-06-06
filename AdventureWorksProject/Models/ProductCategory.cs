using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksProject.Models
{
    [Table("ProductSubcategory", Schema = "Production")]
    public class ProductCategory
    {
        [Key]
        [Column("ProductSubcategoryID")]
        public int ProductCategoryID { get; set; }
        
        [Column("ProductCategoryID")]
        public int ParentCategoryID { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
