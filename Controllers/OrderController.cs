using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SINTIA_DWI_ARGANI.Interfaces;
using SINTIA_DWI_ARGANI.Models;
using SINTIA_DWI_ARGANI.Models.DTO;
using SINTIA_DWI_ARGANI.Services;
using static SINTIA_DWI_ARGANI.Models.GeneralStatus;
using OfficeOpenXml;
using SINTIA_DWI_ARGANI.Models.DB;
using Microsoft.EntityFrameworkCore;
using SINTIA_DWI_ARGANI.Migrations;

namespace SINTIA_DWI_ARGANI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IProduct _product;
        private readonly IOrder _ordering;
        //private readonly IAuthentication _auth; // Add this

        public OrderController(IProduct product, IOrder ordering, IAuthentication auth)
        {
            _product = product;
            _ordering = ordering;
            //_auth = auth;
        }
        //public IActionResult Index()
        //{
        //    var product = _context.Products.FirstOrDefault(); // ❌ ini return Product, bukan List<OrderDTO>
        //    return View(product); // ❌ menyebabkan error seperti yang kamu alami
        //}

        public IActionResult Index()
        {
            var order = _ordering.GetAllOrder();
            return View(order);
        }

        public async Task<IActionResult> ExportToExcel()
        {
            var orders = await _ordering.GetAllHistory(); // Ganti sesuai service/repo kamu
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Orders");

            // Header
            worksheet.Cells[1, 1].Value = "Order ID";
            worksheet.Cells[1, 2].Value = "Product";
            worksheet.Cells[1, 3].Value = "Quantity";
            worksheet.Cells[1, 4].Value = "Total Price";
            worksheet.Cells[1, 5].Value = "Order Date";
            worksheet.Cells[1, 6].Value = "Cashier";
            worksheet.Cells[1, 7].Value = "Status";

            // Isi Data
            int row = 2;
            foreach (var order in orders)
            {
                worksheet.Cells[row, 1].Value = order.Id;
                worksheet.Cells[row, 2].Value = order.ProductName;
                worksheet.Cells[row, 3].Value = order.Quantity;
                worksheet.Cells[row, 4].Value = order.Total;
                worksheet.Cells[row, 5].Value = order.OrderDate.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 6].Value = order.CashierName;
                worksheet.Cells[row, 7].Value = order.StatusOrders == 0 ? "Published" : "Unpublished";
                row++;
            }

            var stream = new MemoryStream(package.GetAsByteArray());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Orders.xlsx");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categori = _ordering.GetAllOrder();
            var product = _product.GetProductById(id);

            // Map Product to ProductDTO
            var productDto = new ProductDTO
            {
                Id = product.Id,
                NameProduct = product.NameProduct,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                IdCategori = product.IdCategori,
                StatusProduct = product.StatusProduct
            };

            return View(productDto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                // Load products and handle empty case
                var products = _product.GetAllProducts();

                if (products == null || !products.Any())
                {
                    TempData["ErrorMessage"] = "No products available. Please add products first.";
                    return RedirectToAction("Index"); // Or handle differently
                }

                ViewBag.Products = new SelectList(products, "Id", "NameProduct");
                ViewBag.ProductPrices = products.ToDictionary(p => p.Id, p => p.Price);

                return View(new OrderDTO
                {
                    OrderDate = DateTime.Now,
                    StatusOrders = GeneralStatusData.published
                });
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error loading create form: {ex.Message}");
                TempData["ErrorMessage"] = "Error loading order form";
                return RedirectToAction("Index");
            }
        }

        //[HttpPost]
        //public IActionResult Create(OrderDTO order)
        //{
        //    try
        //    {
        //        // Validasi manual
        //        if (order.ProductId <= 0)
        //        {
        //            ModelState.AddModelError("ProductId", "Pilih produk yang valid");
        //        }

        //        if (order.Quantity <= 0)
        //        {
        //            ModelState.AddModelError("Quantity", "Quantity harus lebih dari 0");
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            ViewBag.Products = new SelectList(_product.GetAllProducts(), "Id", "NameProduct");
        //            return View(order);
        //        }

        //        // Hitung total jika belum dihitung
        //        if (order.Total <= 0)
        //        {
        //            var product = _product.GetProductById(order.ProductId);
        //            if (product != null)
        //            {
        //                order.Total = product.Price * order.Quantity;
        //            }
        //        }

        //        // Set nilai default
        //        order.OrderDate = DateTime.Now;
        //        order.StatusOrders = GeneralStatus.GeneralStatusData.published;

        //        bool isSuccess = _ordering.AddOrder(order);
        //        Console.WriteLine($"ProductId: {order.ProductId}, Quantity: {order.Quantity}");
        //        if (!isSuccess)
        //        {
        //            TempData["ErrorMessage"] = "Gagal menyimpan order ke database";
        //            ViewBag.Products = new SelectList(_product.GetAllProducts(), "Id", "NameProduct");
        //            return View(order);
        //        }

        //        TempData["SuccessMessage"] = "Order berhasil dibuat!";
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        TempData["ErrorMessage"] = "Terjadi kesalahan sistem";
        //        ViewBag.Products = new SelectList(_product.GetAllProducts(), "Id", "NameProduct");
        //        return View(order);
        //    }
        //}

        public IActionResult Delete(int id)
        {
            var delete = _ordering.DeletedOrder(id);
            if (delete)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Cannot delete this Orders");
        }

        public IActionResult Report()
        {
            // Default report shows last 30 days
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-30);

            var report = _ordering.GenerateReport(startDate, endDate);
            return View(report);
        }

        [HttpPost]
        public IActionResult Report(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                TempData["ErrorMessage"] = "Start date cannot be after end date";
                return RedirectToAction("Report");
            }

            var report = _ordering.GenerateReport(startDate, endDate);
            return View(report);
        }

        [HttpPost]
        public IActionResult Create(OrderDTO order)
        {
            try
            {
                // Reload products for dropdown in case we return to view
                var products = _product.GetAllProducts();
                ViewBag.Products = new SelectList(products, "Id", "NameProduct");
                ViewBag.ProductPrices = products.ToDictionary(p => p.Id, p => p.Price);

                // === VALIDASI ===
                if (order.ProductId <= 0)
                {
                    ModelState.AddModelError("ProductId", "Pilih produk yang valid");
                }

                if (order.Quantity <= 0)
                {
                    ModelState.AddModelError("Quantity", "Jumlah harus lebih dari 0");
                }

                // Get CashierId from session if not provided
                if (order.CashierId <= 0)
                {
                    var cashierId = HttpContext.Session.GetInt32("CashierId");
                    if (cashierId.HasValue)
                    {
                        order.CashierId = cashierId.Value;
                    }
                    else
                    {
                        ModelState.AddModelError("CashierId", "ID Kasir tidak valid");
                    }
                }

                // Get CashierName from session if not provided
                if (string.IsNullOrEmpty(order.CashierName))
                {
                    var cashierName = HttpContext.Session.GetString("CashierName");
                    if (!string.IsNullOrEmpty(cashierName))
                    {
                        order.CashierName = cashierName;
                    }
                    else
                    {
                        ModelState.AddModelError("CashierName", "Nama Kasir tidak ditemukan");
                    }
                }

                // Ambil produk dan cek stok
                var product = _product.GetProductById(order.ProductId);
                if (product == null)
                {
                    ModelState.AddModelError("ProductId", "Produk tidak ditemukan");
                }
                else
                {
                    // Set ProductName
                    order.ProductName = product.NameProduct;

                    // Validasi stok
                    if (product.Stock < order.Quantity)
                    {
                        ModelState.AddModelError("Quantity", $"Stok produk tidak mencukupi. Stok tersedia: {product.Stock}");
                    }
                }

                // Jika tidak valid, kembali ke form
                if (!ModelState.IsValid)
                {
                    return View(order);
                }

                // === PENGOLAHAN ===
                // Hitung total berdasarkan harga produk
                order.Total = product.Price * order.Quantity;
                order.OrderDate = DateTime.Now;
                order.StatusOrders = GeneralStatusData.published;

                // Simpan order
                bool isSuccess = _ordering.AddOrder(order);
                if (!isSuccess)
                {
                    TempData["ErrorMessage"] = "Gagal menyimpan order ke database";
                    return View(order);
                }

                // Kurangi stok produk
                product.Stock -= order.Quantity;

                // Konversi produk ke DTO untuk update
                var productDto = new ProductDTO
                {
                    Id = product.Id,
                    NameProduct = product.NameProduct,
                    Price = product.Price,
                    Stock = product.Stock,
                    Description = product.Description,
                    IdCategori = product.IdCategori,
                    CategoriName = product.Categori?.CategoriName ?? "", // Pastikan ini diisi jika diperlukan
                    StatusProduct = product.StatusProduct
                };

                _product.EditProduct(productDto);

                // === SELESAI ===
                TempData["SuccessMessage"] = "Order berhasil dibuat!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                TempData["ErrorMessage"] = $"Terjadi kesalahan sistem: {ex.Message}";
                return View(order);
            }
        }
    }
}