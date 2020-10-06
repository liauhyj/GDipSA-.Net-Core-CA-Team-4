using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Layout.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Cart()
        {
            ViewData["Is_Cart"] = "menu_hili";
            return View();
        }
    }
}
