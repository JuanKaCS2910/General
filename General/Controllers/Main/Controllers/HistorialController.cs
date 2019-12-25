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
        private ISubTramite oSubTramite;

        public HistorialController()
        {
            this.oPersona = new Persona();
            this.oTipodocumento = new Tipodocumento();
            this.oHistorico = new Historico();
            this.oSubTramite = new SubTramite();
            //this.oHistorico = new 
        }

        // GET: Persona
        public ActionResult Index(Grilla paginacion)
        {
            if (paginacion.countrow == null)
                paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            //ViewBag.Cantidad = paginacion.countrow;
            ViewBag.FiltroDocumentypeId = new SelectList(oTipodocumento.Cargardocumento(),
                "TipodocumentoId", "Descripcion");
            ViewBag.DocumentypeId = new SelectList(oTipodocumento.Cargardocumento(),
                "TipodocumentoId", "Descripcion");

            ViewBag.Frecuencia = new SelectList(oSubTramite.CargarSubtTramite("FR"),
                "SubTramiteId", "Descripcion");

            var result = new ViewModelHistorico
            {
                PersonaGrilla = oPersona.PersonaHistoricoGrillaToPageList(paginacion),
                HistoricoGrilla = null,
                Historicos = new EHistorico(),
                FiltroPerson = new EPersona(),
                cantGrid = paginacion.countrow,
                cantTotal = 0
            };
            result.cantTotal = result.PersonaGrilla.TotalItemCount;
            result.pageView = result.PersonaGrilla.PageNumber;
            return View(result);
        }


        #region Tab1

        [HttpPost]
        public JsonResult CargarBusquedaGrilla(FiltroGrilloPerson Filtro)
        {
            if (Filtro.countrow == null)
                Filtro.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            ViewBag.Cantidad = Filtro.countrow;

            var result = new ViewModelPerson
            {
                PersonaGrilla = oPersona.PersonaHistoricoFoundGrillaToPageList(Filtro),
                Person = new EPersona(),
                FiltroPerson = new EPersona(),
                cantGrid = int.Parse(WebConfigurationManager.AppSettings["CountRow"]),
                cantPage = 0,
                cantTotal = 0
            };

            result.cantTotal = result.PersonaGrilla.TotalItemCount;
            result.cantPage = ((Filtro.countrow > result.cantTotal) == true ? 1 : (result.cantTotal) / ((int)Filtro.countrow) + 1);
            result.pageView = result.PersonaGrilla.PageNumber;
            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Tab2

        [HttpPost]
        public JsonResult SearchHistorico(int idHistorico)
        {
            var result = oHistorico.SearchHistorico(idHistorico);
            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        [HttpPost]
        public JsonResult SearchPerson(FiltroGrilloPerson Filtro)
        {
            var result = oPersona.PersonaFoundPageList(Filtro);

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);

        }

        #region Save,Edit,Delete
        [HttpPost]
        public JsonResult SaveHistorico(EHistorico registro)
        {
            var result = oHistorico.CreateHistory(registro);
            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteHistorico(int idHistorico)
        {
            var result = oHistorico.DeleteHistory(idHistorico);
            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }
        

        #endregion

        #region Historico
        public ActionResult Index1(FiltroGrilloHistorico paginacion)
        {
            if (paginacion.countrow == null)
                paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            ViewBag.DocumentypeId = new SelectList(oTipodocumento.Cargardocumento(),
                "TipodocumentoId", "Descripcion");

            var result = new ViewModelHistorico
            {
                HistoricoGrilla = oHistorico.HistoricoGrillaToPageList(paginacion),
                Historicos = new EHistorico(),
                cantGrid = int.Parse(WebConfigurationManager.AppSettings["CountRow"]),
                cantTotal = 0
            };
            result.cantTotal = result.HistoricoGrilla.TotalItemCount;

            return View(result);
        }

        [HttpPost]
        public JsonResult CargarGrilla(FiltroGrilloHistorico paginacion)
        {
            if (paginacion.countrow == null)
                paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            ViewBag.Cantidad = paginacion.countrow;

            var result = new ViewModelHistorico
            {
                HistoricoGrilla = oHistorico.HistoricoGrillaToPageList(paginacion),
                Historicos = new EHistorico(),
                //FiltroHistoricos = new EPersona(),
                cantGrid = paginacion.countrow,
                cantPage = 0,
                cantTotal = 0
            };

            result.cantTotal = result.HistoricoGrilla.TotalItemCount;
            result.cantPage = ((paginacion.countrow > result.cantTotal) == true ? 1 : (result.cantTotal) / ((int)paginacion.countrow) + 1);
            result.pageView = result.HistoricoGrilla.PageNumber;
            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GrillaPersona
        
        #endregion

        // GET: Historial

    }
}