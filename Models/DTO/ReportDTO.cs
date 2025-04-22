namespace SINTIA_DWI_ARGANI.Models.DTO
{
    public class ReportDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<OrderDTO> Orders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
    }
}