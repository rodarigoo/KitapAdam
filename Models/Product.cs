using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KirtasiyeProjesi.Models
{

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "Ürün adı boş geçilemez.")]
        public string Name { get; set; }

        [Display(Name = "Fiyat")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Stok Adedi")]
        public int Stock { get; set; }


        // Dosya yolunu tutacağız (Örn: /img/kalem.jpg)
        [Display(Name = "Ürün Resmi")]
        public string? ImageUrl { get; set; }

        // --- İLİŞKİLER BAŞLIYOR ---

        // 1. Kategori Bağlantısı
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; } // Hangi kategoriye ait?
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        // 2. Marka Bağlantısı
        [Display(Name = "Marka")]
        public int BrandId { get; set; } // Hangi markaya ait?
        [ForeignKey("BrandId")]
        public virtual Brand? Brand { get; set; }

        // 3. Satış Bağlantısı (Bir ürünün çok satışı olabilir)
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
