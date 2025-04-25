using Microsoft.EntityFrameworkCore;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;

public class OrderingServices : IOrdering
{
    private readonly ApplicationContext _context;

    public OrderingServices(ApplicationContext context)
    {
        _context = context;
    }

    public void CreateOrder(OrderingDTO orderDto)
    {
        throw new NotImplementedException("CreateOrder digantikan oleh OrderingController.");
    }

    public List<OrderingDTO> GetAllOrders()
    {
        var orders = _context.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ToList();

        var dto = orders.Select(o => new OrderingDTO
        {
            CustomerName = o.CustomerName,
            OrderDate = o.OrderDate,
            TotalPrice = o.TotalPrice,
            OrderDetails = o.OrderDetails.Select(d => new OrderDetailDTO
            {
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                ProductName = d.Product?.NameProduct ?? "Unknown Product",
                UnitPrice = d.UnitPrice > 0 ? d.UnitPrice : (d.Product?.Price ?? 0) // Fallback to Product.Price if UnitPrice is 0
            }).ToList()
        }).ToList();

        return dto;
    }

    public List<OrderingDTO> GetOrderReport()
    {
        var orders = _context.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ToList();

        var dto = orders.Select(o => new OrderingDTO
        {
            CustomerName = o.CustomerName,
            OrderDate = o.OrderDate,
            TotalPrice = o.TotalPrice,
            OrderDetails = o.OrderDetails.Select(d => new OrderDetailDTO
            {
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                ProductName = d.Product?.NameProduct ?? "Unknown Product",
                UnitPrice = d.UnitPrice > 0 ? d.UnitPrice : (d.Product?.Price ?? 0) // Fallback to Product.Price if UnitPrice is 0
            }).ToList()
        }).ToList();

        return dto;
    }
}