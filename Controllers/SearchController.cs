using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Layout.Controllers
{
    public class SearchController : Controller
    {
        [HttpPost]
        public IActionResult Search(string keyword)
        {
            return View();
        }
    }
}
