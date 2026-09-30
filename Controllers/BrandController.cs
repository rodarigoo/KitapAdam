using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KirtasiyeProjesi.Data;
using KirtasiyeProjesi.Models;

namespace KirtasiyeProjesi.Controllers
{
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LİSTELEME
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }

        // EKLEME SAYFASI (GET)
        public IActionResult Create()
        {
            return View();
        }

        // EKLEME İŞLEMİ (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {

            ModelState.Remove("Products");

            if (ModelState.IsValid)
            {
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // DÜZENLEME SAYFASI (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        // DÜZENLEME İŞLEMİ (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            if (id != brand.Id) return NotFound();


            ModelState.Remove("Products");

            if (ModelState.IsValid)
            {
                _context.Update(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }
        //(DETAILS) METODU
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);

            if (brand == null) return NotFound();

            return View(brand);
        }


        // SİLME (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var brand = await _context.Brands.FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        // SİLME (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}