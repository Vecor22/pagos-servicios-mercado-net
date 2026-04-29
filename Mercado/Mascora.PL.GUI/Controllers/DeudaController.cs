using Microsoft.AspNetCore.Mvc;
using Mercado.BL.BE;
using Mercado.PL.GUI.DTO.Request;
using Mercado.PL.GUI.DTO.Response;
using Mercado.PL.GUI.Models;

namespace Mercado.PL.GUI.Controllers
{
    public class DeudaController : Controller
    {
        private readonly DeudaModel deudaModel = new DeudaModel();
        private readonly ConceptoCobroModel conceptoCobroModel = new ConceptoCobroModel();
        private readonly PuestoModel puestoModel = new PuestoModel();

        public IActionResult Index()
        {
            var deudas = deudaModel.Listar()
                .Select(d => new DeudaResponse
                {
                    DeudaID = d.DeudaID,
                    CodigoDeuda = d.CodigoDeuda,
                    Monto = d.Monto,
                    Estado = d.Estado,
                    TipoGeneracion = d.TipoGeneracion,
                    Observacion = d.Observacion,
                    FechaGeneracion = d.FechaGeneracion,
                    Concepto = d.ConceptoCobro?.Nombre ?? string.Empty,
                    CodigoPuesto = d.Puesto?.CodigoPuesto,
                    Socio = d.Socio?.Nombres,
                    FechaExoneracion = d.FechaExoneracion,
                    MotivoExoneracion = d.MotivoExoneracion
                })
                .ToList();

            return View(deudas);
        }

        public IActionResult Crear()
        {
            CargarListasCrear();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(DeudaRequest request)
        {
            try
            {
                request.UsuarioID = ObtenerUsuarioIdSesion();

                DeudaBE deuda = new DeudaBE
                {
                    ConceptoCobro = new ConceptoCobroBE
                    {
                        ConceptoCobroID = request.ConceptoCobroID
                    },
                    Puesto = string.IsNullOrWhiteSpace(request.CodigoPuesto)
                        ? null
                        : new PuestoBE { CodigoPuesto = request.CodigoPuesto },
                    Monto = request.Monto,
                    TipoGeneracion = request.TipoGeneracion,
                    Observacion = request.Observacion,
                    CreadoPor = new UsuarioBE
                    {
                        UsuarioID = request.UsuarioID
                    }
                };

                deudaModel.Crear(deuda);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                CargarListasCrear();
                return View(request);
            }
        }

        public IActionResult Exonerar()
        {
            CargarListasExonerar();
            return View();
        }

        [HttpPost]
        public IActionResult Exonerar(DeudaExonerarRequest request)
        {
            try
            {
                request.UsuarioID = ObtenerUsuarioIdSesion();
                deudaModel.Exonerar(request.DeudaID, request.Motivo, request.UsuarioID);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                CargarListasExonerar();
                return View(request);
            }
        }

        private void CargarListasCrear()
        {
            ViewBag.Conceptos = conceptoCobroModel.Listar();
            ViewBag.Puestos = puestoModel.Listar();
        }

        private void CargarListasExonerar()
        {
            ViewBag.DeudasPendientes = deudaModel.ListarPorEstado("PENDIENTE");
        }

        private long ObtenerUsuarioIdSesion()
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioID");

            if (!long.TryParse(usuarioId, out var id))
                throw new Exception("Usuario no autenticado.");

            return id;
        }
    }
}
