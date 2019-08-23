using Aplication.Services.Logica.Mantenimiento;
using Domain.Entities.General;
using System.Web.Configuration;
using System.Web.Mvc;


namespace General.Controllers.Main.Controllers
{
    public class DistritoController : Controller
    {
        private Distrito oDistrito;

        public DistritoController()
        {
            this.oDistrito = new Distrito();
        }

        // GET: Distrito
        public ActionResult Index(Grilla paginacion)
        {
            paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);
            var result = oDistrito.DistritoGrillaToPageList(paginacion);

            return View(result);
        }
    }
}