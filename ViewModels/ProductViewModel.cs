using KirtasiyeProjesi.Models;
using Microsoft.AspNetCore.Http;

namespace KirtasiyeProjesi.ViewModels
{
    public class ProductViewModel
    {
        // Asıl Ürün Bilgileri
        public Product Product { get; set; }

        // Resim Dosyası (Sadece yükleme için, veritabanına gitmez)
        public IFormFile? ImageFile { get; set; }
    }
}