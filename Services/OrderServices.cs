using Microsoft.EntityFrameworkCore;
using SINTIA_DWI_ARGANI.Models.DTO;
using SINTIA_DWI_ARGANI.Models;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models.DB;


namespace SINTIA_DWI_ARGANI.Services
{
    public class OrderServices : IOrder
    {
        private readonly ApplicationContext _context;
        private object cashier;

        public OrderServices(ApplicationContext context)
        {
            _context = context;
        }

        public ReportDTO GenerateReport(DateTime startDate, DateTime endDate)
        {
            var report = new ReportDTO
            {
                StartDate = startDate,
                EndDate = endDate
            };

            // Get orders within date range
            report.Orders = _context.Ordering
                .Include(o => o.Product)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    //ProductName = o.Product.NameProduct,
                    Quantity = o.Quantity,
                    Total = o.Total,
                    OrderDate = o.OrderDate,
                    StatusOrders = o.StatusOrders
                })
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            // Calculate summary
            report.TotalOrders = report.Orders.Count;
            report.TotalRevenue = report.Orders.Sum(o => o.Total);

            return report;
        }
        public List<OrderDTO> GetAllOrders()
        {
            return _context.Ordering
                .Include(o => o.Product)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    //ProductName = o.Product.NameProduct,
                    Quantity = o.Quantity,
                    Total = o.Total,
                    OrderDate = o.OrderDate,
                    //CashierName = o.CashierName,
                    StatusOrders = o.StatusOrders
                })
                .ToList();
        }
        public List<OrderDTO> GetAllOrder()
        {
            var orders = _context.Ordering
                .Include(y => y.Product)
                .Where(x => x.StatusOrders != GeneralStatusData.deleted)
                .Select(x => new OrderDTO
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Total = x.Total,
                    OrderDate = x.OrderDate,
                    CashierName = x.Cashier.Name,
                    StatusOrders = x.StatusOrders
                }).ToList();
            return orders;
        }

        public OrderDTO GetOrderById(int id)
        {
            var order = _context.Ordering
                .Include(o => o.Product)
                .Where(x => x.Id == id && x.StatusOrders != GeneralStatusData.deleted)
                .FirstOrDefault();

            if (order == null)
            {
                return null;
            }

            // Konversi Order ke OrderDTO
            return new OrderDTO
            {
                Id = order.Id,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                Total = order.Total,
                OrderDate = order.OrderDate,
                //CashierName = order.CashierName,
                StatusOrders = order.StatusOrders
            };
        }

        //public bool AddOrder(OrderDTO orderDto)
        //{
        //    using var transaction = _context.Database.BeginTransaction();
        //    try
        //    {
        //        // Validasi
        //        if (orderDto == null || orderDto.ProductId <= 0 || orderDto.Quantity <= 0)
        //            return false;

        //        // Dapatkan produk
        //        var product = _context.Products.Find(orderDto.ProductId);
        //        if (product == null) return false;

        //        // Cek stok
        //        if (product.Stock < orderDto.Quantity)
        //        {
        //            Console.WriteLine("Stok tidak mencukupi");
        //            return false;
        //        }

        //        // Buat order
        //        var order = new Order
        //        {
        //            ProductId = orderDto.ProductId,
        //            Quantity = orderDto.Quantity,
        //            Total = product.Price * orderDto.Quantity,
        //            OrderDate = DateTime.Now,
        //            //CashierName = orderDto.CashierName ?? "System",
        //            StatusOrders = GeneralStatus.GeneralStatusData.published
        //        };

        //        // Kurangi stok
        //        product.Stock -= orderDto.Quantity;

        //        _context.Ordering.Add(order);
        //        _context.Products.Update(product);

        //        int affected = _context.SaveChanges();
        //        transaction.Commit();

        //        return affected > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.Rollback();
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return false;
        //    }
        //}
        // In your OrderService implementation
        //public bool AddOrder(OrderDTO orderDto)
        //{
        //    //try
        //    //{
        //    //    var order = new Order
        //    //    {
        //    //        ProductId = orderDto.ProductId,
        //    //        Quantity = orderDto.Quantity,
        //    //        Total = orderDto.Total,
        //    //        OrderDate = orderDto.OrderDate,
        //    //        CashierId = orderDto.CashierId,
        //    //        StatusOrders = orderDto.StatusOrders
        //    //    };
        //    //    System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {orderDto.ProductId}");
        //    //    System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {orderDto.ProductName}");
        //    //    System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {orderDto.Total}");
        //    //    System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {orderDto.OrderDate}");
        //    //    System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {orderDto.CashierId}");
        //    //    System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {orderDto.StatusOrders}");

        //    //    _context.Ordering.Add(order);
        //    //    _context.SaveChanges(); // This is crucial
        //    //    return true;
        //    //}
        //    //catch
        //    //{
        //    //    return false;
        //    //}

        //    var data = new Order();
        //    data.ProductId = orderDto.ProductId;
        //    data.Quantity = orderDto.Quantity;
        //    data.Total = orderDto.Total;
        //    data.OrderDate = orderDto.OrderDate;
        //    data.CashierId = orderDto.CashierId;
        //    data.StatusOrders = orderDto.StatusOrders;

        //    System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {orderDto.ProductId}");
        //    System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {orderDto.ProductName}");
        //    System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {orderDto.Total}");
        //    System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {orderDto.OrderDate}");
        //    System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {orderDto.CashierId}");
        //    System.Diagnostics.Debug.WriteLine($"Percobaan login untuk: {orderDto.StatusOrders}");

        //    _context.Add(data);
        //    _context.SaveChanges();
        //    return true;
        //}

        public bool AddOrder(OrderDTO orderDto)
        {
            try
            {
                // Basic validation
                if (orderDto == null || orderDto.ProductId <= 0 || orderDto.Quantity <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("Invalid order data");
                    return false;
                }

                // Check if product exists and has enough stock
                var product = _context.Products
                    .FirstOrDefault(p => p.Id == orderDto.ProductId && p.StatusProduct != GeneralStatusData.deleted);
                System.Diagnostics.Debug.WriteLine($"ProductId : {product.Id}");
                if (product == null)
                {
                    System.Diagnostics.Debug.WriteLine("Product not found");
                    return false;
                }

                if (product.Stock < orderDto.Quantity)
                {
                    System.Diagnostics.Debug.WriteLine("Insufficient stock");
                    return false;
                }

                // Create new order
                var order = new Order
                {
                    ProductId = orderDto.ProductId,
                    Quantity = orderDto.Quantity,
                    Total = orderDto.Total, // Assuming this is calculated correctly
                    OrderDate = orderDto.OrderDate,
                    CashierId = orderDto.CashierId,
                    StatusOrders = orderDto.StatusOrders
                };

                product.Stock -= order.Quantity;
                // Save the order
                _context.Ordering.Add(order);
                _context.SaveChanges();

                System.Diagnostics.Debug.WriteLine("Order successfully added");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding order: {ex.Message}");
                return false;
            }
        }

        public bool UpdateOrder(OrderDTO order)
        {
            var existingOrder = _context.Ordering.FirstOrDefault(x => x.Id == order.Id);

            if (existingOrder == null)
            {
                return false;
            }

            // Update nilai-nilai yang diperlukan
            existingOrder.ProductId = order.ProductId;
            existingOrder.Quantity = order.Quantity;
            existingOrder.Total = order.Total;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.StatusOrders = order.StatusOrders;

            _context.SaveChanges();
            return true;
        }

        public bool DeletedOrder(int Id)
        {
            var order = _context.Ordering.FirstOrDefault(x => x.Id == Id);
            if (order != null && order.StatusOrders != GeneralStatusData.deleted)
            {
                order.StatusOrders = GeneralStatusData.deleted;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        // In your OrderService class that implements IOrder
        public Cashier GetCashierByName(string name)
        {
            var cashierAccess = _context.Cashiers.FirstOrDefault(c => c.Name == name);
            if (cashierAccess == null) return null;

            // Map CashierAccess to Cashier if necessary
            Cashier cashier = new Cashier
            {
                // Assuming CashierAccess and Cashier have similar properties
                Id = cashierAccess.Id,
                Name = cashierAccess.Name,
                // Add other properties accordingly
            };

            return cashier;
        }

        public async Task<List<OrderDTO>> GetAllHistory()
        {
            var orders = await _context.Ordering
                .Include(o => o.Cashier)
                .Include(o => o.Product)
                .Where(x => x.StatusOrders != GeneralStatusData.deleted)
                .ToListAsync();

            var orderDTOs = orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                ProductId = o.ProductId,
                ProductName = o.Product.NameProduct, // pastikan ini ada di DTO
                Quantity = o.Quantity,
                Total = o.Total,
                OrderDate = o.OrderDate,
                CashierName = o.Cashier.Name, // pastikan ini ada di DTO
                StatusOrders = o.StatusOrders
            }).ToList();

            return orderDTOs;
        }
    }
}