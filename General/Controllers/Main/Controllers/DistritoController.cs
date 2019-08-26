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
        [HttpPost]
        public ActionResult Grabar(EDistrito view)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    oDistrito.CreateDistrito(view);
                }
                catch (Exception ex)
                {
                    string error = string.Empty;

                    if (ex.InnerException != null &&
                        ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("Index"))
                    {
                        error = "El campo ya se encuentra registrado";
                        
                    }
                    else
                    {
                        error = ex.Message;
                    }
                    return RedirectToAction("Index");//, new { message = error });
                }
            }
            return RedirectToAction("Index");
            //return View(result);
        }

        // GET: Distrito
        public ActionResult Index(Grilla paginacion)//, string message)
        {
            //ModelState.AddModelError(string.Empty, "Error");
            //if (!string.IsNullOrEmpty(message))
            //{
            //    ViewBag.Error = message;
            //    //ModelState.AddModelError(string.Empty, "Error Fail");
            //}

            paginacion.countrow = int.Parse(WebConfigurationManager.AppSettings["CountRow"]);
            
            var result = oDistrito.DistritoGrillaToPageList(paginacion);
            
            return View(result);
        }
    }
}