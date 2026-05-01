using Microsoft.AspNetCore.Mvc;
using Mercado.BL.BE;
using Mercado.PL.GUI.DTO.Request;
using Mercado.PL.GUI.DTO.Response;
using Mercado.PL.GUI.Models;

namespace Mercado.PL.GUI.Controllers
{
    public class ConceptoCobroController : Controller
    {
        private readonly ConceptoCobroModel conceptoCobroModel = new ConceptoCobroModel();

        public IActionResult Index()
        {
            var conceptos = conceptoCobroModel.Listar()
                .Select(c => new ConceptoCobroResponse
                {
                    ConceptoCobroID = c.ConceptoCobroID,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion,
                    TipoCobro = c.TipoCobro,
                    FechaCreacion = c.FechaCreacion,
                    FechaActualizacion = c.FechaActualizacion
                })
                .ToList();

            return View(conceptos);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(ConceptoCobroRequest request)
        {
            try
            {
                ConceptoCobroBE concepto = new ConceptoCobroBE
                {
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    TipoCobro = request.TipoCobro
                };

                conceptoCobroModel.Crear(concepto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(request);
            }
        }

        public IActionResult Editar(long id)
        {
            var concepto = conceptoCobroModel.BuscarPorId(id);

            if (concepto == null)
                return NotFound();

            var request = new ConceptoCobroRequest
            {
                Nombre = concepto.Nombre,
                Descripcion = concepto.Descripcion,
                TipoCobro = concepto.TipoCobro
            };

            ViewBag.ConceptoCobroID = concepto.ConceptoCobroID;

            return View(request);
        }

        [HttpPost]
        public IActionResult Editar(long id, ConceptoCobroRequest request)
        {
            try
            {
                ConceptoCobroBE concepto = new ConceptoCobroBE
                {
                    ConceptoCobroID = id,
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    TipoCobro = request.TipoCobro
                };

                conceptoCobroModel.Actualizar(concepto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ConceptoCobroID = id;
                return View(request);
            }
        }
    }
}
