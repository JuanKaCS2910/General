using Aplication.Services.Interfaz;
using Aplication.Services.Logica.Mantenimiento;
using Domain.Entities.Mantenimiento;
using System.Collections.Generic;
using System.Web.Mvc;

namespace General.Controllers
{
    public class LoginController : Controller
    {
        private IUsuario oUsuario;

        public LoginController()
        {
            this.oUsuario = new Usuario();
        }

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EUsuario data)//int? n)
        {
            List<EUsuario> _result = oUsuario.ObtenerPersona(data);

            if (_result.Count > 0)
            {
                Session["data"] = _result;
                return RedirectToAction("Main", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Cerrar()
        {
            Session.Remove("data");
            return RedirectToAction("Index", "Login");
        }
    }
}