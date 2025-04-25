using System.ComponentModel.DataAnnotations;

namespace SINTIA_DWI_ARGANI.Models.DTO
{
    public class OrderDetailDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; } 
    }
}
