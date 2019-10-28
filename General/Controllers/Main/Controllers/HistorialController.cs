﻿using Aplication.Services.Interfaz;
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
        #region Save,Edit,Delete
        [HttpPost]
        public JsonResult SaveHistorico(EHistorico registro)
        {
            var result = oHistorico.CreateHistory(registro);
            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        // GET: Historial
        public ActionResult Index(Grilla paginacion)
        {
            if (paginacion.countrow == null)
                paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            ViewBag.DocumentypeId = new SelectList(oTipodocumento.Cargardocumento(),
                "TipodocumentoId", "Descripcion");

            var result = new ViewModelHistorico {
                HistoricoGrilla = oHistorico.HistoricoGrillaToPageList(paginacion),
                Historicos = new EHistorico(),
                cantGrid = int.Parse(WebConfigurationManager.AppSettings["CountRow"]),
                cantTotal = 0
            };
            result.cantTotal = result.HistoricoGrilla.TotalItemCount;

            return View(result);
        }

        [HttpPost]
        public JsonResult CargarGrilla(Grilla paginacion)
        {
            if (paginacion.countrow == null)
                paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            ViewBag.Cantidad = paginacion.countrow;

            var result = new ViewModelHistorico
            {
                HistoricoGrilla = oHistorico.HistoricoGrillaToPageList(paginacion),
                Historicos = new EHistorico(),
                //FiltroHistoricos = new EPersona(),
                cantGrid = int.Parse(WebConfigurationManager.AppSettings["CountRow"]),
                cantPage = 0,
                cantTotal = 0
            };

            result.cantTotal = result.HistoricoGrilla.TotalItemCount;

            result.cantPage = ((paginacion.countrow > result.cantTotal) == true ? 1 : (result.cantTotal) / ((int)paginacion.countrow) + 1);

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }
    }
}