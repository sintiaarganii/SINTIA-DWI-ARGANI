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
        public virtual DbSet<Order> Ordering { get; set; }
        public virtual DbSet<Cashier> Auth { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Categori)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.IdCategori);

            /// Relasi antara Product dan Ordering
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orderings)  // Product memiliki banyak Ordering
                .WithOne(o => o.Product)  // Ordering hanya punya satu Product
                .HasForeignKey(o => o.ProductId);  // Menggunakan ProductId sebagai foreign key

            modelBuilder.Entity<Order>()
            .HasOne(o => o.Cashier)
            .WithMany() // jika Cashier tidak punya List<Order>
            .HasForeignKey(o => o.CashierId);

            // Gunakan nilai dari konfigurasi, bukan hardcode
            var pepper = _configuration["Security:Pepper"];
            var iteration = Convert.ToInt32(_configuration["Security:Iteration"]);
            var salt = Hasher.GenerateSalt();

            modelBuilder.Entity<Cashier>().HasData(
                new Cashier
                {
                    Id = 1, // Ubah dari 3 ke 1 untuk konsistensi
                    Name = "Administrator",
                    Username = "admin",
                    Salt = salt, // Gunakan salt yang baru digenerate
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
