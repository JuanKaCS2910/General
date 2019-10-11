using Aplication.Services.Interfaz;
using Aplication.Services.Logica.Mantenimiento;
using Domain.Entities.General;
using Domain.Entities.Mantenimiento;
using Domain.Entities.ViewModel;
using PagedList;
using System.Web.Configuration;
using System.Web.Mvc;

namespace General.Controllers.Main.Controllers
{
    public class HistorialController : Controller
    {
        private IPersona oPersona;
        private IHistorico oHistorico;
        private ITipodocumento oTipodocumento;

        public HistorialController()
        {
            this.oPersona = new Persona();
            this.oTipodocumento = new Tipodocumento();
            this.oHistorico = new Historico();
            //this.oHistorico = new 
        }

        [HttpPost]
        public JsonResult SearchPerson(FiltroGrilloPerson Filtro)
        {
            var result = oPersona.PersonaFoundPageList(Filtro);

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);

        }

        // GET: Historial
        public ActionResult Index(Grilla paginacion)
        {
            if (paginacion.countrow == null)
                paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            ViewBag.DocumentypeId = new SelectList(oTipodocumento.Cargardocumento(),
                "TipodocumentoId", "Descripcion");

            var model = new ViewModelHistorico {
                Historicos = new EHistorico(),
            };

            return View(model);
        }
    }
}