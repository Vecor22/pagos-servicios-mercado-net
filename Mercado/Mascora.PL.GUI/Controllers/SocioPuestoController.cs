using Microsoft.AspNetCore.Mvc;
using Mercado.PL.GUI.DTO.Request;
using Mercado.PL.GUI.DTO.Response;
using Mercado.PL.GUI.Models;

namespace Mercado.PL.GUI.Controllers
{
    public class SocioPuestoController : Controller
    {
        private readonly SocioPuestoModel socioPuestoModel = new SocioPuestoModel();
        private readonly SocioModel socioModel = new SocioModel();
        private readonly PuestoModel puestoModel = new PuestoModel();

        public IActionResult Index()
        {
            var asignaciones = socioPuestoModel.Listar()
                .Select(sp => new SocioPuestoResponse
                {
                    CodigoSocio = sp.Socio.CodigoSocio,
                    NombreSocio = $"{sp.Socio.Nombres} {sp.Socio.Apellidos}",
                    Dni = sp.Socio.Dni,
                    CodigoPuesto = sp.Puesto.CodigoPuesto,
                    AsignadoPor = sp.AsignadoPor?.NombreCompleto ?? string.Empty,
                    FechaAsignacion = sp.FechaAsignacion
                })
                .ToList();

            return View(asignaciones);
        }

        public IActionResult Asignar()
        {
            CargarListasAsignacion();
            return View();
        }

        [HttpPost]
        public IActionResult Asignar(SocioPuestoAsignacionRequest request)
        {
            try
            {
                request.UsuarioID = ObtenerUsuarioIdSesion();
                socioPuestoModel.CrearAsignacion(request.CodigoSocio, request.CodigoPuesto, request.UsuarioID);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                CargarListasAsignacion();
                return View(request);
            }
        }

        public IActionResult Reasignar()
        {
            CargarListasReasignacion();
            return View();
        }

        [HttpPost]
        public IActionResult Reasignar(SocioPuestoReasignacionRequest request)
        {
            try
            {
                request.UsuarioID = ObtenerUsuarioIdSesion();
                socioPuestoModel.Reasignar(request.CodigoSocio, request.CodigoPuesto, request.UsuarioID);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                CargarListasReasignacion();
                return View(request);
            }
        }

        private void CargarListasAsignacion()
        {
            var socios = socioModel.Listar()
                .Where(s => s.Estado == "ACTIVO")
                .ToList();

            var codigosAsignados = socioPuestoModel.Listar()
                .Select(sp => sp.Puesto.CodigoPuesto)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var puestosDisponibles = puestoModel.Listar()
                .Where(p => !codigosAsignados.Contains(p.CodigoPuesto))
                .ToList();

            ViewBag.Socios = socios;
            ViewBag.Puestos = puestosDisponibles;
        }

        private void CargarListasReasignacion()
        {
            var socios = socioModel.Listar()
                .Where(s => s.Estado == "ACTIVO")
                .ToList();

            var puestosAsignados = socioPuestoModel.Listar()
                .Select(sp => sp.Puesto)
                .GroupBy(p => p.CodigoPuesto)
                .Select(g => g.First())
                .ToList();

            ViewBag.Socios = socios;
            ViewBag.Puestos = puestosAsignados;
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
