using Aplication.Services.Interfaz.Mantenimiento;
using Aplication.Services.Logica.Mantenimiento;
using Domain.Entities.General;
using Domain.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace General.Controllers
{
    //https://www.youtube.com/watch?v=gbMHN2EVpfM
    public class HomeController : Controller
    {
        private IPersonaService oIPersonaService;

        public HomeController()
        {
            this.oIPersonaService = new PersonaService();
        }
        
        public ActionResult Main()
        {
            var Main = new HMain()
            {
                Personas = oIPersonaService.PersonaGrilla(null).Count(),
                //Historicos = oIHistorico.PersonaHistoricoGrilla(null).Count()
            };

            return View(Main);
        }

    }
}