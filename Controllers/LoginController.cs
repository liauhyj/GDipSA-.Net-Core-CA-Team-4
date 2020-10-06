using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Layout.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            ViewData["Is_login"] = "menu_hili";
            return View();
        }
    }
}
