using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KirtasiyeProjesi.Data;
using KirtasiyeProjesi.Models;
using KirtasiyeProjesi.ViewModels; 
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace KirtasiyeProjesi.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // 1. LİSTELEME (VİTRİN) - Burası Müşteri İçin
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Brand).Include(p => p.Category).ToListAsync();
            return View(products);
        }

        // 2. YÖNETİM PANELİ (LİSTE)
        public async Task<IActionResult> Manage()
        {
            var products = await _context.Products.Include(p => p.Brand).Include(p => p.Category).ToListAsync();
            return View(products);
        }

        // 3. DETAY
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // 4. EKLEME SAYFASI (GET)
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View(); // Boş bir sayfa açar
        }

        // 5. EKLEME İŞLEMİ (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel vm)
        {
           
            ModelState.Remove("Product.Sales");
            ModelState.Remove("Product.Category");
            ModelState.Remove("Product.Brand");
            ModelState.Remove("Product.ImageUrl"); 

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (vm.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "img");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await vm.ImageFile.CopyToAsync(fileStream);
                    }
                }

                vm.Product.ImageUrl = uniqueFileName;
                _context.Add(vm.Product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage));
            }

            
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", vm.Product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", vm.Product.CategoryId);
            return View(vm);
        }
        // 6. DÜZENLEME SAYFASI (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            // Modeli ViewModel'e çeviriyoruz ki View hata vermesin
            var vm = new ProductViewModel
            {
                Product = product
            };

            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(vm);
        }

        // 7. DÜZENLEME İŞLEMİ (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel vm)
        {
            if (id != vm.Product.Id) return NotFound();

           
            ModelState.Remove("Product.Sales");
            ModelState.Remove("Product.Category");
            ModelState.Remove("Product.Brand");
            ModelState.Remove("Product.ImageUrl"); 

            if (ModelState.IsValid)
            {
                try
                {
                    // Eski veriyi çekiyoruz (Resim değişmediyse eskisini korumak için)
                    var oldProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

                    if (vm.ImageFile != null)
                    {
                        // Yeni resim yüklenmiş
                        string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "img");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.ImageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await vm.ImageFile.CopyToAsync(fileStream);
                        }
                        vm.Product.ImageUrl = uniqueFileName;
                    }
                    else
                    {
                        // Resim yüklenmemiş, eskisini kullan
                        vm.Product.ImageUrl = oldProduct.ImageUrl;
                    }

                    _context.Update(vm.Product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.Id == vm.Product.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Manage));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", vm.Product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", vm.Product.CategoryId);
            return View(vm);
        }

        // 8. SİLME SAYFASI (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // 9. SİLME İŞLEMİ (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Manage));
        }
    }
}