# KitapAdam – Kırtasiye Yönetim ve Satış Uygulaması

ASP.NET Core MVC (.NET 8) ile geliştirilmiş, kitap ve kırtasiye ürünlerinin listelendiği ve satışların takip edildiği bir web uygulamasıdır. Müşteriler için bir ürün vitrini, mağaza sahibi için bir yönetim paneli sunar.

## Özellikler

- **Ürün vitrini:** Ürünler marka ve kategori bilgisiyle listelenir; ana sayfada son eklenen 4 ürün gösterilir.
- **Yönetim paneli:** Ürün ekleme, düzenleme, silme ve detay görüntüleme.
- **Kategori ve marka yönetimi:** Tam CRUD işlemleri.
- **Satış kaydı:** Ürün ve adet seçildiğinde toplam tutar (birim fiyat × adet) otomatik hesaplanır.
- **Ürün görseli yükleme:** Görseller benzersiz dosya adıyla `wwwroot/img` klasörüne kaydedilir; düzenlemede yeni görsel seçilmezse mevcut görsel korunur.
- **Doğrulama:** Zorunlu alanlar için sunucu tarafı doğrulama ve anti-forgery token kullanımı.

## Kullanılan Teknolojiler

- C#, ASP.NET Core MVC (.NET 8), Razor
- Entity Framework Core 8 (Code-First, Migrations)
- SQL Server (LocalDB)
- Bootstrap, Bootstrap Icons
- Docker (Dockerfile)

## Veritabanı Yapısı

- `Category` (1) — (N) `Product`
- `Brand` (1) — (N) `Product`
- `Product` (1) — (N) `Sale`

Parasal alanlar (`Price`, `TotalPrice`) kuruş hassasiyeti için `decimal(18,2)` olarak tanımlanmıştır.

## Çalıştırma

Gereksinimler: .NET 8 SDK ve SQL Server LocalDB (Visual Studio ile gelir).

```bash
git clone https://github.com/rodarigoo/KitapAdam.git
cd KitapAdam
dotnet ef database update
dotnet run
```

Bağlantı dizesi `appsettings.json` dosyasındaki `DefaultConnection` alanındadır. Gerekirse kendi SQL Server bilgilerinize göre değiştirin.

## Proje Yapısı

```
Controllers/   Ana sayfa, kategori, marka kontrolcüleri
Data/          ApplicationDbContext
Migrations/    EF Core migration dosyaları
Models/        Product, Category, Brand, Sale
ViewModels/    ProductViewModel (görsel yükleme için)
Views/         Razor görünümleri
wwwroot/       Statik dosyalar ve ürün görselleri
```

## Geliştirici

Mehmet Ziya Duman – Bilgisayar Mühendisliği, Biruni Üniversitesi
