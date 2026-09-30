using KirtasiyeProjesi.Models;
using Microsoft.EntityFrameworkCore;

namespace KirtasiyeProjesi.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

        // Kuruş Hassasiyeti Ayarı ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ürün fiyatı: Virgülden önce 18 basamak, virgülden sonra 2 basamak (kuruş)
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Satış toplam tutarı: Virgülden önce 18 basamak, virgülden sonra 2 basamak
            modelBuilder.Entity<Sale>()
                .Property(s => s.TotalPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}