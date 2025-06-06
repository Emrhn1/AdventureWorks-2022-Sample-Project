using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdventureWorksProject.Models
{
    [Table("SalesOrderDetail", Schema = "Sales")]
    public class SalesOrderDetail
    {
        [Key]
        [Column("SalesOrderDetailID")]
        public int SalesOrderDetailID { get; set; }
        public int SalesOrderID { get; set; }
        public int ProductID { get; set; }
        [Column("OrderQty")]
        public short OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal LineTotal { get; private set; }
        [ForeignKey("ProductID")]
        public Product? Product { get; set; }
        [ForeignKey("SalesOrderID")]
        public SalesOrderHeader? SalesOrderHeader { get; set; }
    }
}
