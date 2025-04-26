namespace SINTIA_DWI_ARGANI.Models.DB
{
    public class Ordering
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; } // Tambahkan TotalPrice

        //public string CashierId { get; set; } // New field to store cashier's user ID
        //public Cashier Cashier { get; set; } // Navigation property

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}