using SINTIA_DWI_ARGANI.Models.DB;

namespace SINTIA_DWI_ARGANI.Models.DTO
{
    public class OrderingViewDTO
    {
        public int OrderId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public decimal TotalPrice { get; set; } 

    }
}
