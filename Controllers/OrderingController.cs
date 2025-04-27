using Microsoft.AspNetCore.Mvc;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;
using System.Diagnostics;

public class OrderingController : BaseController
{
    private readonly ApplicationContext _context;
    private readonly IOrdering _orderingService;

    public OrderingController(ApplicationContext context, IOrdering orderingService)
    {
        _context = context;
        _orderingService = orderingService;

    }

    public IActionResult Index()
    {
        var orders = _orderingService.GetAllOrders();
        Debug.WriteLine($"Jumlah pesanan yang diambil: {orders.Count}");
        return View(orders);
    }

    public IActionResult Report()
    {
        var orders = _orderingService.GetOrderReport();
        Debug.WriteLine($"Jumlah pesanan untuk laporan: {orders.Count}");
        return View(orders);
    }

    public IActionResult Create()
    {
        var model = new OrderingDTO();
        // Fetch only published products
        var products = (from product in _context.Products
                        where product.StatusProduct == 0 // Only Published products
                        select new ProductDTO
                        {
                            Id = product.Id,
                            NameProduct = product.NameProduct,
                            Stock = product.Stock,
                            StatusProduct = product.StatusProduct
                            // Include other properties as needed
                        }).ToList();

        ViewBag.Products = products;
        return View(model);
    }

    [HttpPost]
    public IActionResult Create(OrderingDTO dto)
    {
        Debug.WriteLine("=== Mulai Create (POST) ===");
        Debug.WriteLine($"CustomerName: {dto.CustomerName}");
        Debug.WriteLine($"OrderDetails Count: {dto.OrderDetails?.Count ?? 0}");
        if (dto.OrderDetails != null)
        {
            foreach (var detail in dto.OrderDetails)
            {
                Debug.WriteLine($"ProductId: {detail.ProductId}, Quantity: {detail.Quantity}, ProductName: {detail.ProductName}");
            }
        }
        else
        {
            Debug.WriteLine("OrderDetails adalah null!");
            ModelState.AddModelError("", "Tidak ada produk yang dipilih.");
        }

        if (dto.OrderDetails == null || !dto.OrderDetails.Any())
        {
            Debug.WriteLine("Validasi gagal: Tidak ada produk yang dipilih!");
            ModelState.AddModelError("", "Harus ada minimal satuproduk dalam pesanan.");
        }

        if (!ModelState.IsValid)
        {
            Debug.WriteLine("ModelState tidak valid. Berikut adalah error:");
            foreach (var error in ModelState)
            {
                foreach (var err in error.Value.Errors)
                {
                    Debug.WriteLine($"Error pada {error.Key}: {err.ErrorMessage}");
                }
            }

            ViewBag.Products = _context.Products
                .Where(p => p.StatusProduct == GeneralStatus.GeneralStatusData.published)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    NameProduct = p.NameProduct,
                    Price = p.Price,
                    Stock = p.Stock,
                    StatusProduct = p.StatusProduct
                })
                .ToList();
            return View(dto);
        }

        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                Debug.WriteLine("Mengambil data produk untuk validasi stok...");
                var productIds = dto.OrderDetails.Select(d => d.ProductId).ToList();
                var products = _context.Products
                    .Where(p => productIds.Contains(p.Id))
                    .ToList();

                foreach (var detail in dto.OrderDetails)
                {
                    var product = products.FirstOrDefault(p => p.Id == detail.ProductId);
                    if (product == null)
                    {
                        throw new Exception($"Produk dengan ID {detail.ProductId} tidak ditemukan.");
                    }

                    if (product.StatusProduct != GeneralStatus.GeneralStatusData.published)
                    {
                        throw new Exception($"Produk {product.NameProduct} tidak dapat di-order karena statusnya bukan 'published'.");
                    }

                    if (product.Stock == 0)
                    {
                        throw new Exception($"Produk {product.NameProduct} tidak dapat di-order karena stok habis (0).");
                    }

                    if (product.Stock < detail.Quantity)
                    {
                        throw new Exception($"Stok untuk produk {product.NameProduct} tidak cukup. Stok tersedia: {product.Stock}, Dibutuhkan: {detail.Quantity}");
                    }
                }

                decimal totalPrice = 0;
                foreach (var detail in dto.OrderDetails)
                {
                    var product = products.First(p => p.Id == detail.ProductId);
                    totalPrice += product.Price * detail.Quantity;
                }

                Debug.WriteLine("Membuat entitas Ordering...");
                var order = new Ordering
                {
                    CustomerName = dto.CustomerName,
                    OrderDate = DateTime.Now,
                    TotalPrice = totalPrice
                };

                Debug.WriteLine("Menambahkan order ke context...");
                _context.Orders.Add(order);

                Debug.WriteLine("Menyimpan Ordering ke database...");
                int orderChanges = _context.SaveChanges();
                Debug.WriteLine($"Jumlah perubahan (Ordering): {orderChanges}, OrderId: {order.Id}");

                Debug.WriteLine("Membuat dan menambahkan OrderDetails...");
                var orderDetails = dto.OrderDetails.Select(x => new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = products.First(p => p.Id == x.ProductId).Price // Set UnitPrice
                }).ToList();

                _context.OrderDetails.AddRange(orderDetails);

                Debug.WriteLine("Menyimpan OrderDetails ke database...");
                int detailChanges = _context.SaveChanges();
                Debug.WriteLine($"Jumlah perubahan (OrderDetails): {detailChanges}");

                Debug.WriteLine("Mengurangi stok produk...");
                foreach (var detail in dto.OrderDetails)
                {
                    var product = products.First(p => p.Id == detail.ProductId);
                    product.Stock -= detail.Quantity;
                    _context.Products.Update(product);
                }

                Debug.WriteLine("Menyimpan perubahan stok ke database...");
                int stockChanges = _context.SaveChanges();
                Debug.WriteLine($"Jumlah perubahan (Stock): {stockChanges}");

                Debug.WriteLine("Commit transaksi...");
                transaction.Commit();

                Debug.WriteLine("Mengalihkan ke halaman Index...");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Terjadi kesalahan saat menyimpan ke database: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                Debug.WriteLine("Rollback transaksi...");
                transaction.Rollback();

                ModelState.AddModelError("", $"Terjadi kesalahan saat menyimpan pesanan: {ex.Message}");
                ViewBag.Products = _context.Products
                    .Where(p => p.StatusProduct == GeneralStatus.GeneralStatusData.published)
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        NameProduct = p.NameProduct,
                        Price = p.Price,
                        Stock = p.Stock,
                        StatusProduct = p.StatusProduct
                    })
                    .ToList();
                return View(dto);
            }
        }
    }
}