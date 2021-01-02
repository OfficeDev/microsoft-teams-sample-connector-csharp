using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamsToDoAppConnector.Controllers
{
    public class AuthenticationController : Controller
    {
       

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

        [HttpGet]
        public ActionResult Logout()
        {
            return View();
        }

    }
}