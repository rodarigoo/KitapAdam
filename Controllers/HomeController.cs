using KirtasiyeProjesi.Data; // Veritabaný eriþimi
using KirtasiyeProjesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KirtasiyeProjesi.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; // Veritabanýný ekledik

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            var vitrinUrunleri = _context.Products
                                         .Include(p => p.Category)
                                         .OrderByDescending(p => p.Id)
                                         .OrderByDescending(p => p.Id)
                                         .Take(4)
                                         .ToList();

            return View(vitrinUrunleri);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}