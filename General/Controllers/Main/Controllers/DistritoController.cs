using Aplication.Services.Logica.Mantenimiento;
using Domain.Entities.General;
using Domain.Entities.Mantenimiento;
using System.Web.Configuration;
using System.Web.Mvc;
using System;
using Aplication.Services.Interfaz;
using System.Threading.Tasks;
using System.Configuration;
using Domain.Entities.ViewModel;

namespace General.Controllers.Main.Controllers
{
    public class DistritoController : Controller
    {
        private IDistrito oDistrito;

        public DistritoController()
        {
            this.oDistrito = new Distrito();
        }

        public JsonResult Edit(int distritoId)
        {
            var result = oDistrito.DistritoGrilla(distritoId);

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
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

        public async Task<JsonResult> Actualizar(EDistrito view)
        {
            string sResultado = string.Empty;
            await Task.Delay(1);
            sResultado = oDistrito.UpdateDistrito(view);
            return Json(new { Resultado = sResultado }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index");
        }

        public JsonResult CargarGrilla(Grilla paginacion)
        {
            if (paginacion.countrow == null)
                paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            ViewBag.Cantidad = paginacion.countrow;

            var result = oDistrito.DistritoGrillaToPageList(paginacion);

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }

        // GET: Distrito
        public ActionResult Index(Grilla paginacion)
        {
            if (Session["data"] == null)
                return RedirectToAction("Index", "Login");

            if (paginacion.countrow == null)
                paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);

            ViewBag.Cantidad = paginacion.countrow;

            var result = new ViewModelDistrito
            {
                Url = ConfigurationManager.AppSettings["Url"],
                DistritoGrilla = oDistrito.DistritoGrillaToPageList(paginacion),
                Distrito = new EDistritoView()
            };

            //var result = oDistrito.DistritoGrillaToPageList(paginacion);
            
            return View(result);
        }
    }
}