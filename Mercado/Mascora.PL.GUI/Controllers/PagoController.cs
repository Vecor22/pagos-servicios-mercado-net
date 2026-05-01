using Mercado.BL.BE;
using Mercado.DL.DALC;
using Mercado.PL.GUI.DTO.Request;
using Mercado.PL.GUI.DTO.Response;
using Mercado.PL.GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Mercado.PL.GUI.Controllers
{
    public class PagoController : Controller
    {
        private readonly PagoModel pagoModel = new PagoModel();
        private readonly DeudaModel deudaModel = new DeudaModel();

        public IActionResult Index(string? estado, string? codigoPuesto, string? codigoDeuda, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var datosPago = ObtenerDatosPago();

            var pagosBase = string.IsNullOrWhiteSpace(estado)
                ? pagoModel.Listar()
                : pagoModel.ListarPorEstado(estado.Trim());

            var pagos = pagosBase
                .Select(p =>
                {
                    datosPago.TryGetValue(p.PagoID, out var detalle);
                    return MapearPagoResponse(p, detalle);
                })
                .ToList();

            if (!string.IsNullOrWhiteSpace(codigoPuesto))
            {
                pagos = pagos
                    .Where(p => string.Equals(p.CodigoPuesto, codigoPuesto.Trim(), StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(codigoDeuda))
            {
                pagos = pagos
                    .Where(p => string.Equals(p.CodigoDeuda, codigoDeuda.Trim(), StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (fechaInicio.HasValue)
            {
                pagos = pagos
                    .Where(p => p.FechaPago.Date >= fechaInicio.Value.Date)
                    .ToList();
            }

            if (fechaFin.HasValue)
            {
                pagos = pagos
                    .Where(p => p.FechaPago.Date <= fechaFin.Value.Date)
                    .ToList();
            }

            ViewBag.Estado = estado;
            ViewBag.CodigoPuesto = codigoPuesto;
            ViewBag.CodigoDeuda = codigoDeuda;
            ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd");

            pagos = pagos
                .OrderByDescending(p => p.FechaPago)
                .ToList();

            return View(pagos);
        }

        public IActionResult Crear()
        {
            return View(new PagoRequest());
        }

        public IActionResult Detalle(long id)
        {
            var pago = ObtenerPagoResponse(id);

            if (pago == null)
                return NotFound();

            return View(pago);
        }

        public IActionResult VerComprobante(long id)
        {
            TempData["Error"] = "La vista de comprobante aun no esta implementada.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult BuscarDeuda(string codigoDeuda)
        {
            var request = new PagoRequest
            {
                CodigoDeuda = codigoDeuda?.Trim() ?? string.Empty
            };

            if (string.IsNullOrWhiteSpace(request.CodigoDeuda))
                return MostrarVistaCrear(request, null, "Debe ingresar un codigo de deuda.");

            var deuda = deudaModel.BuscarPorCodigo(request.CodigoDeuda);

            if (deuda == null)
                return MostrarVistaCrear(request, null, "La deuda no existe.");

            if (!string.Equals(deuda.Estado, "PENDIENTE", StringComparison.OrdinalIgnoreCase))
                return MostrarVistaCrear(request, null, "Solo se puede registrar pago para una deuda con estado PENDIENTE.");

            return MostrarVistaCrear(request, deuda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(PagoRequest request)
        {
            var codigoDeuda = request.CodigoDeuda?.Trim() ?? string.Empty;
            var medioPago = request.MedioPago?.Trim() ?? string.Empty;
            var numeroOperacion = string.IsNullOrWhiteSpace(request.NumeroOperacion)
                ? null
                : request.NumeroOperacion.Trim();

            try
            {
                if (string.IsNullOrWhiteSpace(codigoDeuda))
                    throw new Exception("Debe seleccionar una deuda valida.");

                if (string.IsNullOrWhiteSpace(medioPago))
                    throw new Exception("Debe seleccionar un medio de pago.");

                var deuda = deudaModel.BuscarPorCodigo(codigoDeuda);

                if (deuda == null)
                    throw new Exception("La deuda no existe.");

                if (!string.Equals(deuda.Estado, "PENDIENTE", StringComparison.OrdinalIgnoreCase))
                    throw new Exception("Solo se puede registrar pago para una deuda con estado PENDIENTE.");

                request.UsuarioID = ObtenerUsuarioIdSesion();
                pagoModel.Crear(codigoDeuda, medioPago, numeroOperacion, request.UsuarioID);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                request.CodigoDeuda = codigoDeuda;
                request.MedioPago = medioPago;
                request.NumeroOperacion = numeroOperacion;

                var deuda = string.IsNullOrWhiteSpace(codigoDeuda)
                    ? null
                    : deudaModel.BuscarPorCodigo(codigoDeuda);

                if (deuda != null && !string.Equals(deuda.Estado, "PENDIENTE", StringComparison.OrdinalIgnoreCase))
                    deuda = null;

                return MostrarVistaCrear(request, deuda, ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Anular(PagoAnularRequest request)
        {
            try
            {
                request.UsuarioID = ObtenerUsuarioIdSesion();
                pagoModel.Anular(request.PagoID, request.Motivo, request.UsuarioID);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var pago = ObtenerPagoResponse(request.PagoID);

                if (pago == null)
                    return NotFound();

                ViewBag.Pago = pago;
                ViewBag.Error = ex.Message;

                return View(request);
            }
        }

        public IActionResult Anular(long id)
        {
            var pago = ObtenerPagoResponse(id);

            if (pago == null)
                return NotFound();

            if (!string.Equals(pago.Estado, "REGISTRADO", StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "Solo se puede anular un pago en estado REGISTRADO.";
                return RedirectToAction("Index");
            }

            ViewBag.Pago = pago;

            return View(new PagoAnularRequest
            {
                PagoID = pago.PagoID
            });
        }

        private IActionResult MostrarVistaCrear(PagoRequest? request, DeudaBE? deuda, string? error = null)
        {
            ViewBag.Deuda = deuda;
            ViewBag.Error = error;
            return View("Crear", request ?? new PagoRequest());
        }

        private PagoResponse? ObtenerPagoResponse(long pagoId)
        {
            var pago = pagoModel.BuscarPorId(pagoId);

            if (pago == null)
                return null;

            var datosPago = ObtenerDatosPago();
            datosPago.TryGetValue(pagoId, out var detalle);

            return MapearPagoResponse(pago, detalle);
        }

        private static Dictionary<long, PagoDetalle> ObtenerDatosPago()
        {
            var datos = new Dictionary<long, PagoDetalle>();

            using var cn = Conexion.getConnection();
            using var cmd = new SqlCommand(@"
                SELECT
                    p.pago_id,
                    d.codigo_deuda,
                    pu.codigo_puesto,
                    CONCAT(s.nombres, ' ', s.apellidos) AS socio,
                    cc.nombre AS concepto,
                    p.fecha_anulacion,
                    p.motivo_anulacion,
                    ur.nombre_completo AS usuario_registro,
                    ua.nombre_completo AS usuario_anulacion
                FROM Pago p
                INNER JOIN Deuda d ON p.deuda_id = d.deuda_id
                INNER JOIN ConceptoCobro cc ON d.concepto_cobro_id = cc.concepto_cobro_id
                LEFT JOIN Puesto pu ON d.puesto_id = pu.puesto_id
                LEFT JOIN Socio s ON d.socio_id = s.socio_id
                INNER JOIN Usuario ur ON p.registrado_por_usuario_id = ur.usuario_id
                LEFT JOIN Usuario ua ON p.anulado_por_usuario_id = ua.usuario_id", cn);

            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var pagoId = Convert.ToInt64(dr["pago_id"]);

                datos[pagoId] = new PagoDetalle
                {
                    CodigoDeuda = dr["codigo_deuda"]?.ToString() ?? string.Empty,
                    CodigoPuesto = dr["codigo_puesto"] == DBNull.Value ? null : dr["codigo_puesto"].ToString(),
                    Socio = dr["socio"] == DBNull.Value ? null : dr["socio"].ToString(),
                    Concepto = dr["concepto"] == DBNull.Value ? null : dr["concepto"].ToString(),
                    FechaAnulacion = dr["fecha_anulacion"] == DBNull.Value ? null : Convert.ToDateTime(dr["fecha_anulacion"]),
                    MotivoAnulacion = dr["motivo_anulacion"] == DBNull.Value ? null : dr["motivo_anulacion"].ToString(),
                    UsuarioRegistro = dr["usuario_registro"] == DBNull.Value ? null : dr["usuario_registro"].ToString(),
                    UsuarioAnulacion = dr["usuario_anulacion"] == DBNull.Value ? null : dr["usuario_anulacion"].ToString()
                };
            }

            return datos;
        }

        private static PagoResponse MapearPagoResponse(PagoBE pago, PagoDetalle? detalle)
        {
            return new PagoResponse
            {
                PagoID = pago.PagoID,
                CodigoPago = pago.CodigoPago,
                CodigoDeuda = detalle?.CodigoDeuda ?? string.Empty,
                CodigoPuesto = detalle?.CodigoPuesto,
                Socio = detalle?.Socio,
                Concepto = detalle?.Concepto,
                MontoPagado = pago.MontoPagado,
                MedioPago = pago.MedioPago,
                NumeroOperacion = pago.NumeroOperacion,
                Estado = pago.Estado,
                FechaPago = pago.FechaPago,
                FechaAnulacion = detalle?.FechaAnulacion ?? pago.FechaAnulacion,
                MotivoAnulacion = detalle?.MotivoAnulacion ?? pago.MotivoAnulacion,
                UsuarioRegistro = detalle?.UsuarioRegistro,
                UsuarioAnulacion = detalle?.UsuarioAnulacion
            };
        }

        private long ObtenerUsuarioIdSesion()
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioID");

            if (!long.TryParse(usuarioId, out var id))
                throw new Exception("Usuario no autenticado.");

            return id;
        }

        private sealed class PagoDetalle
        {
            public string CodigoDeuda { get; set; } = string.Empty;
            public string? CodigoPuesto { get; set; }
            public string? Socio { get; set; }
            public string? Concepto { get; set; }
            public DateTime? FechaAnulacion { get; set; }
            public string? MotivoAnulacion { get; set; }
            public string? UsuarioRegistro { get; set; }
            public string? UsuarioAnulacion { get; set; }
        }
    }
}
