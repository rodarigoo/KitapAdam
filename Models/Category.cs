using System.ComponentModel.DataAnnotations;

namespace KirtasiyeProjesi.Models
{

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        public string Name { get; set; }

        // İLİŞKİ (1-M): Bir Kategorinin içinde 'Çokça' ürün olabilir.

        public virtual ICollection<Product> Products { get; set; }
    }
}