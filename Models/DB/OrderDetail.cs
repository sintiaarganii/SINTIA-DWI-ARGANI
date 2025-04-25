namespace SINTIA_DWI_ARGANI.Models.DB
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // Properti yang hilang, akan ditambahkan

        public virtual Ordering Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
