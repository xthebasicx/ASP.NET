﻿using Microsoft.AspNetCore.Mvc;
using ASP.NET.Context;
using ASP.NET.Models;
using Microsoft.EntityFrameworkCore;


namespace ASP.NET.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _db;

        public CartController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var username = User.Identity.Name;
            var user = _db.Users.Include(u => u.Cart).ThenInclude(c => c.Products).FirstOrDefault(u => u.Username == username);

            if (user?.Cart != null)
            {
                return View(user.Cart);
            }

            return View(new Cart());
        }

        public IActionResult AddToCart(int id)
        {
            var username = User.Identity.Name;
            var user = _db.Users.Include(u => u.Cart).ThenInclude(c => c.Products).FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                var product = _db.Products.Find(id);
                if (product != null)
                {
                    if (user.Cart == null)
                    {
                        user.Cart = new Cart();
                        _db.Carts.Add(user.Cart);
                    }

                    if (!user.Cart.Products.Any(p => p.ProductID == product.ProductID))
                    {
                        user.Cart.Products.Add(product);
                        _db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int productId)
        {
            var username = User.Identity.Name;
            var user = _db.Users.Include(u => u.Cart).ThenInclude(c => c.Products).FirstOrDefault(u => u.Username == username);

            if (user?.Cart != null)
            {
                var product = user.Cart.Products.FirstOrDefault(p => p.ProductID == productId);
                if (product != null)
                {
                    user.Cart.Products.Remove(product);
                    _db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

    }
}
