using static SINTIA_DWI_ARGANI.Models.GeneralStatus;

public class OrderDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }
    public string CashierName { get; set; }
    public int CashierId { get; set; } // Add this
    public GeneralStatusData StatusOrders { get; set; }
    public string ProductName { get; set; } // For display purposes
}