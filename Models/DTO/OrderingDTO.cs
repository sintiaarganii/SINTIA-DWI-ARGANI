using System.ComponentModel.DataAnnotations;

namespace SINTIA_DWI_ARGANI.Models.DTO
{
    public class OrderingDTO
    {
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; } 
        public List<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
    }

}
