using System.Web.Mvc;

namespace TeamsToDoAppConnector.Controllers
{
    /// <summary>
    /// Represents the controller responsible for start and end page of authentication.
    /// </summary>
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
    }
}