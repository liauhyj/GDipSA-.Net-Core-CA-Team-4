using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Layout.Db;
using Layout.Models;
using Microsoft.AspNetCore.Mvc;

namespace Layout.Controllers
{
    public class CartController : Controller
    {
        private readonly Database db;

        public CartController(Database db)
        {
            this.db = db;
        }
        private Cart GetCart()
        {
            Cart cart = (Cart)Session.Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session.Cart = cart;
            }
            return cart;
        }

        private List<Product> GetAllProducts()
        {
            return new List<Product>()
            {
                
                
                new Product(){Id = 1, Description = "dsafawerf",Image = "/images/Paylah.jpg",Name = "产品1",Price = 85},
                new Product(){Id = 2, Description = "safewq",Image = "/images/Charts.jpg",Name = "产品2",Price = 95},
                new Product(){Id = 3, Description = "qweqwesda",Image = "/images/ML.jpg",Name = "产品3",Price = 55},
                new Product(){Id = 4, Description = "zxcas",Image = "/images/Analytics.jpg",Name = "产品4",Price = 65},
                new Product(){Id = 5, Description = "qweefd",Image = "/images/Logger.jpg",Name = "产品5",Price = 75}
            };
        }

        //购物车页
        public IActionResult CartIndex(Cart cart, string returnUrl)
        {
            return View(new CartIndexVm
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        //添加到购物车
        public IActionResult AddToCart(Cart cart, int id, string returnUrl)
        {
            Product product = GetAllProducts().Where(p => p.Id == id).FirstOrDefault();
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("CartIndex", new { returnUrl });
        }

        

        //点击数量+号或点击数量-号或自己输入一个值
        [HttpPost]
        public ActionResult IncreaseOrDecreaseOne(Cart cart, int id, int quantity)
        {
            Product product = GetAllProducts().Where(p => p.Id == id).FirstOrDefault();
            if (product != null)
            {
                cart.IncreaseOrDecreaseOne(product, quantity);
            }
            return Json(new
            {
                msg = true
            });
        }

        //从购物车移除
        public ActionResult RemoveFromCart(Cart cart, int id, string returnUrl)
        {
            Product product = GetAllProducts().Where(p => p.Id == id).FirstOrDefault();
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("CartIndex", new { returnUrl });
        }

        //清空购物车
        public ActionResult EmptyCart(Cart cart, string returnUrl)
        {
            cart.Clear();
            return View("Index", new ProductsListVm { Products = GetAllProducts() });
        }
        //显示购物车摘要
        public ActionResult Summary(Cart cart)
        {
            return View(cart);
        }
    }
}
