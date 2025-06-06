using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksProject.Models
{
    [Table("Customer", Schema = "Sales")]
    public class Customer
    {
        [Key]
        [Column("CustomerID")]
        public int CustomerID { get; set; }
        [NotMapped]
        public string FirstName { get; set; } = string.Empty;
        [NotMapped]
        public string LastName { get; set; } = string.Empty;
        
        
        public ICollection<SalesOrderHeader> Orders { get; set; } = new List<SalesOrderHeader>();
    }
}
