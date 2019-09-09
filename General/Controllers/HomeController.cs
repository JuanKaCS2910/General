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

        public HomeController()
        {
            this.oIPersona = new Persona();
        }

        public ActionResult Main()
        {
            var Main = new HMain()
            {
                Personas = oIPersona.PersonaGrilla(null).Count()
            };

            return View(Main);
        }

    }
}