using ASP.NET.Context;
using ASP.NET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _db;


        public HomeController(ILogger<HomeController> logger,DataContext db)
        {
            _db = db;
            _logger = logger;

        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products;
            return View(products);
        }
        public IActionResult Detail(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
