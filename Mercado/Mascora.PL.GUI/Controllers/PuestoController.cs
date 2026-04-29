using Microsoft.AspNetCore.Mvc;
using Mercado.PL.GUI.DTO.Response;
using Mercado.PL.GUI.Models;

namespace Mercado.PL.GUI.Controllers
{
    public class PuestoController : Controller
    {
        private readonly PuestoModel puestoModel = new PuestoModel();

        public IActionResult Index()
        {
            var puestos = puestoModel.Listar()
                .Select(p => new PuestoResponse
                {
                    PuestoID = p.PuestoID,
                    CodigoPuesto = p.CodigoPuesto,
                    FechaCreacion = p.FechaCreacion
                })
                .ToList();

            return View(puestos);
        }

        [HttpPost]
        public IActionResult Crear()
        {
            try
            {
                puestoModel.Crear();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                var puestos = puestoModel.Listar()
                    .Select(p => new PuestoResponse
                    {
                        PuestoID = p.PuestoID,
                        CodigoPuesto = p.CodigoPuesto,
                        FechaCreacion = p.FechaCreacion
                    })
                    .ToList();

                return View("Index", puestos);
            }
        }
    }
}
