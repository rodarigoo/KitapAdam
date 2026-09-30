using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KirtasiyeProjesi.Models
{

    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Satış Tarihi")]
        public DateTime Date { get; set; } = DateTime.Now; // Otomatik şu anki tarih

        [Display(Name = "Satılan Adet")]
        public int Quantity { get; set; }

        [Display(Name = "Toplam Tutar")]
        public decimal TotalPrice { get; set; }

        // İLİŞKİ: Hangi ürün satıldı?
        [Display(Name = "Satılan Ürün")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}