using Aplication.Services.Interfaz;
using Aplication.Services.Logica.Mantenimiento;
using Domain.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace General.Controllers
{
    public class HomeController : Controller
    {
        private IPersona oIPersona;
        private IHistorico oIHistorico;

        public HomeController()
        {
            this.oIPersona = new Persona();
            this.oIHistorico = new Historico();
        }

        public ActionResult Main()
        {
            if (Session["data"] == null)
                return RedirectToAction("Index", "Login");

            var Main = new HMain()
            {
                Personas = oIPersona.PersonaGrilla(null).Count(),
                Historicos = oIHistorico.PersonaHistoricoGrilla(null).Count()
            };

            return View(Main);

        }

    }
}