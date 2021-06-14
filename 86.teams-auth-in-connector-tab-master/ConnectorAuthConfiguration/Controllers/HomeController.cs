using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ConnectorAuthConfiguration.Models;

namespace ConnectorAuthConfiguration.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController()
        {
            
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }


        [HttpGet]
        public ViewResult Setup()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SimpleStart()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SimpleEnd()
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
