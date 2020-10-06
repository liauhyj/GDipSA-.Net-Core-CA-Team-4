using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Layout.Models;

namespace Layout.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            ViewData["Is_Product"] = "menu_hili";
            return View();
        }
       
        

      
       
        
    }
}
