using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using KirtasiyeProjesi.Data;
using KirtasiyeProjesi.Models;
using System;
using System.Linq;

namespace KirtasiyeProjesi.Models
    // Veya Models, senin namespace neyse
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Veritabanı yoksa oluştur
                context.Database.EnsureCreated();

                // Eğer içeride zaten ürün varsa ekleme yapma
                if (context.Products.Any())
                {
                    return;
                }

                // --- 1. KATEGORİLERİ OLUŞTUR ---
                var catKirtasiye = new Category { Name = "Kırtasiye" };
                var catOyuncak = new Category { Name = "Oyuncak" };
                var catKitap = new Category { Name = "Kitap" };

                context.Categories.AddRange(catKirtasiye, catOyuncak, catKitap);
                context.SaveChanges(); // Kaydet ki ID'leri oluşsun

                // --- 2. MARKALARI OLUŞTUR (Senin İstediğin İsimler) ---
                var brandSticky = new Brand { Name = "Sticky" };
                var brandBlok = new Brand { Name = "BLOK" };
                var brandApex = new Brand { Name = "Apex-Studio" };
                var brandNetis = new Brand { Name = "Netis Yayıncılık" };

                context.Brands.AddRange(brandSticky, brandBlok, brandApex, brandNetis);
                context.SaveChanges();

                // --- 3. ÜRÜNLERİ EKLE ---
                context.Products.AddRange(
                    // 1. Ürün: Yapıştırıcı
                    new Product
                    {
                        Name = "Sticky Yapıştırıcı 22Gr",
                        Price = 70.00m,
                        Stock = 100,
                        ImageUrl = "sticky_yapistirici.png", // Resim adını değiştirmeyi unutma
                        CategoryId = catKirtasiye.Id,
                        BrandId = brandSticky.Id
                    },

                    // 2. Ürün: Polis Arabası
                    new Product
                    {
                        Name = "BLOK City Polis Arabası",
                        Price = 469.00m,
                        Stock = 10,
                        ImageUrl = "blok_polis.png",
                        CategoryId = catOyuncak.Id,
                        BrandId = brandBlok.Id
                    },

                    // 3. Ürün: Kalem Seti
                    new Product
                    {
                        Name = "Apex-Studio Triclick Versatil Kalem+min Seti 0.7 Mm",
                        Price = 75.00m,
                        Stock = 120,
                        ImageUrl = "apex_kalem.jpg",
                        CategoryId = catKirtasiye.Id,
                        BrandId = brandApex.Id
                    },

                    // 4. Ürün: Kitap
                    new Product
                    {
                        Name = "Sauron Yüzüğün Efendisi",
                        Price = 1088.00m,
                        Stock = 10,
                        ImageUrl = "sauron_kitap.jpg",
                        CategoryId = catKitap.Id,
                        BrandId = brandNetis.Id
                    }
                );

                context.SaveChanges(); // Hepsini veritabanına işle
            }
        }
    }
}