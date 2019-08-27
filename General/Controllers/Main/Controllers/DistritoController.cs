using Aplication.Services.Logica.Mantenimiento;
using Domain.Entities.General;
using Domain.Entities.Mantenimiento;
using System.Web.Configuration;
using System.Web.Mvc;
using System;


namespace General.Controllers.Main.Controllers
{
    public class DistritoController : Controller
    {
        private Distrito oDistrito;

        public DistritoController()
        {
            this.oDistrito = new Distrito();
        }

        public JsonResult Grabar(EDistrito view)
        {
            string sResultado = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    oDistrito.CreateDistrito(view);
                    sResultado = "OK";
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                        ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("Index"))
                    {
                        sResultado = "El campo ya se encuentra registrado";

                    }
                    else
                    {
                        sResultado = ex.Message;
                    }
                }
            }

            return Json(new { Resultado = sResultado }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int distritoId)
        {
            string sResultado = string.Empty;
            sResultado = oDistrito.EliminarDistrito(distritoId);

            return Json(new { Resultado = sResultado }, JsonRequestBehavior.AllowGet);
        }

        // GET: Distrito
        public ActionResult Index(Grilla paginacion)
        {
            if (paginacion.countrow == null)
                paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            ViewBag.Cantidad = paginacion.countrow;

            var result = oDistrito.DistritoGrillaToPageList(paginacion);
            
            return View(result);
        }
    }
}