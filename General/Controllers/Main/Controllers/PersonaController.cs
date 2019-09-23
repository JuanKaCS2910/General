using Aplication.Services.Interfaz;
using Aplication.Services.Logica.Mantenimiento;
using Domain.Entities.General;
using Domain.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace General.Controllers.Main.Controllers
{
    public class PersonaController : Controller
    {
        private IPersona oPersona;
        private ITipodocumento oTipodocumento;
        private ISexo oSexo;
        private IDistrito oDistrito;

        public PersonaController()
        {
            this.oPersona = new Persona();
            this.oTipodocumento = new Tipodocumento();
            this.oSexo = new Sexo();
            this.oDistrito = new Distrito();
        }

        #region Save,Edit,Delete
        [HttpPost]
        public JsonResult SavePerson(EPersona registro)
        {
            var result = oPersona.CreatePerson(registro);
            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditPerson(EPersona registro)
        {
            var result = oPersona.EditPerson(registro);
            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletePerson(EPersona registro)
        {
            var result = oPersona.DeletePerson(registro);
            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        [HttpPost]
        public JsonResult CargarPerson(int? personaId)
        {
            var result = oPersona.PersonaGrilla(personaId);
            ViewBag.DocumentypeId = new SelectList(oTipodocumento.Cargardocumento(),
                "TipodocumentoId", "Descripcion",result.FirstOrDefault().TipodocumentoId);
            ViewBag.SexoId = new SelectList(oSexo.Cargarsexo(),
                "SexoId", "Descripcion",result.FirstOrDefault().SexoId);
            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CargarFiltro(FiltroDistritoPersona filtro)
        {
            //if (paginacion.countrow == null)
            //    paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            //ViewBag.Cantidad = paginacion.countrow;

            var result = oDistrito.BusquedaDistrito(filtro);

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CargarGrilla(Grilla paginacion)
        {
            if (paginacion.countrow == null)
                paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            ViewBag.Cantidad = paginacion.countrow;

            //var result = oPersona.PersonaGrillaToPageList(paginacion);
            var result = new ViewModelPerson
            {
                PersonaGrilla = oPersona.PersonaGrillaToPageList(paginacion),
                Person = new EPersona(),
                FiltroPerson = new EPersona(),
                cantGrid = int.Parse(WebConfigurationManager.AppSettings["CountRow"]),
                cantPage = 0,
                cantTotal = 0
            };
            
            result.cantTotal = result.PersonaGrilla.TotalItemCount;

            //if (paginacion.countrow > result.cantTotal)
            //{
            //    result.cantPage = 1;
            //}
            //else
            //{
            //    result.cantPage = (result.cantTotal / (int)paginacion.countrow);
            //}
            result.cantPage = ((paginacion.countrow > result.cantTotal) == true ? 1 : (result.cantTotal)/((int)paginacion.countrow) + 1);

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }

        // GET: Persona
        public ActionResult Index(Grilla paginacion)
        {
            if (paginacion.countrow == null)
                paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            //ViewBag.Cantidad = paginacion.countrow;
            ViewBag.DocumentypeId = new SelectList(oTipodocumento.Cargardocumento(),
                "TipodocumentoId", "Descripcion");
            ViewBag.SexoId = new SelectList(oSexo.Cargarsexo(),
                "SexoId", "Descripcion");

            var result = new ViewModelPerson
            {
                PersonaGrilla = oPersona.PersonaGrillaToPageList(paginacion),
                Person = new EPersona(),
                FiltroPerson = new EPersona(),
                cantGrid = int.Parse(WebConfigurationManager.AppSettings["CountRow"]),
                cantTotal = 0
            };
            result.cantTotal = result.PersonaGrilla.TotalItemCount;
            //var result = oPersona.PersonaGrillaToPageList(paginacion);

            return View(result);
        }
    }
}