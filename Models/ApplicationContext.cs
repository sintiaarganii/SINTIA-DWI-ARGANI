using Microsoft.EntityFrameworkCore;
using SINTIA_DWI_ARGANI.Helper;
using SINTIA_DWI_ARGANI.Models.DB;
using SINTIA_DWI_ARGANI.Models.DTO;

namespace SINTIA_DWI_ARGANI.Models
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public virtual DbSet<CashierAccess> Cashiers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categoris {  get; set; }
        public virtual DbSet<Cashier> Auth { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Ordering> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Categori)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.IdCategori);

            // Relasi antara Product dan OrderDetail (bukan Ordering)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orderings) // Product memiliki banyak OrderDetail
                .WithOne(o => o.Product)  // OrderDetail hanya punya satu Product
                .HasForeignKey(o => o.ProductId); // Menggunakan ProductId sebagai foreign key

            // Konfigurasi relasi antara Ordering dan OrderDetail
            modelBuilder.Entity<Ordering>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.Orderings)
                .HasForeignKey(od => od.ProductId);

            // Pastikan OrderId di OrderDetail tidak boleh null
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.OrderId)
                .IsRequired();

            // Konfigurasi Stock di Product
            modelBuilder.Entity<Product>()
                .Property(p => p.Stock)
                .IsRequired();

            var pepper = _configuration["Security:Pepper"];
            var iteration = Convert.ToInt32(_configuration["Security:Iteration"]);
            var salt = Hasher.GenerateSalt();

            modelBuilder.Entity<Cashier>().HasData(
                new Cashier
                {
                    Id = 1,
                    Name = "Administrator",
                    Username = "admin",
                    Salt = salt, 
                    PasswordHash = Hasher.ComputeHash("admin123", salt, pepper, iteration),
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    CashierStatus = GeneralStatus.GeneralStatusData.published
                }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}
