using System.ComponentModel.DataAnnotations;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;

namespace SINTIA_DWI_ARGANI.Models.DB
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public int CashierId { get; set; } // foreign key
        public Cashier Cashier { get; set; } // navigation property
        public GeneralStatusData StatusOrders { get; set; }
        public Product Product { get; set; } // Navigation property
    }
}