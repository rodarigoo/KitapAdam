using System.ComponentModel.DataAnnotations;

namespace KirtasiyeProjesi.Models
{

    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Marka Adı")]
        [Required(ErrorMessage = "Marka adı zorunludur.")]
        public string Name { get; set; }

        // İLİŞKİ (1-M): Bir Markanın 'Çokça' ürünü olabilir.
        public virtual ICollection<Product> Products { get; set; }
    }
}