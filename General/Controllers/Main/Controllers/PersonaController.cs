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

        [HttpPost]
        public JsonResult SavePerson(EPersona registro)
        {
            var result = oPersona.CreatePerson(registro);
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
                Person = new EPersona()
            };

            //var result = oPersona.PersonaGrillaToPageList(paginacion);

            return View(result);
        }
    }
}