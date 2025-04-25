namespace SINTIA_DWI_ARGANI.Models.DB
{
    public class Ordering
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; } // Tambahkan TotalPrice

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}