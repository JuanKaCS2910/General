using System.Web.Mvc;

namespace General.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int? n)
        {
            return RedirectToAction("Main", "Home");
        }
    }
}