using ASP.NET.Context;
using ASP.NET.Models;
using ASP.NET.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataContext _db;
        IWebHostEnvironment _hc;
        public AdminController(DataContext db, IWebHostEnvironment hc)
        {
            _db = db;
            _hc = hc;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products;
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Productviewsmodel product)
        {
            if (ModelState.IsValid)
            {
                var uploadsFolder = Path.Combine(_hc.WebRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + product.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.Image.CopyToAsync(fileStream);
                }

                var P = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    KeyGame = product.KeyGame,
                    Image = "/images/" + uniqueFileName
                };

                _db.Products.Add(P);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            var viewModel = new Productviewsmodel
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                KeyGame = product.KeyGame
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Productviewsmodel product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _db.Products.Find(product.ProductID);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.KeyGame = product.KeyGame;

                if (product.Image != null)
                {
                    var uploadsFolder = Path.Combine(_hc.WebRootPath, "images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + product.Image.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.Image.CopyToAsync(fileStream);
                    }

                    existingProduct.Image = "/images/" + uniqueFileName;
                }

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            var imagePath = Path.Combine(_hc.WebRootPath, product.Image.TrimStart('/'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _db.Products.Remove(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
