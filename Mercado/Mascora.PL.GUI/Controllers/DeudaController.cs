using Microsoft.AspNetCore.Mvc;
using Mercado.BL.BE;
using Mercado.PL.GUI.DTO.Request;
using Mercado.PL.GUI.DTO.Response;
using Mercado.PL.GUI.Models;
using Mercado.DL.DALC;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Mercado.PL.GUI.Controllers
{
    public class DeudaController : Controller
    {
        private readonly DeudaModel deudaModel = new DeudaModel();
        private readonly ConceptoCobroModel conceptoCobroModel = new ConceptoCobroModel();
        private readonly PuestoModel puestoModel = new PuestoModel();

        public IActionResult Index(string? estado, DateTime? fechaInicio, DateTime? fechaFin, string? codigoPuesto, string? codigoDeuda)
        {
            var deudas = deudaModel.Listar();

            var datosDeuda = ObtenerDatosDeuda();

            var response = deudas
                .Select(d => MapearDeudaResponse(d, datosDeuda.TryGetValue(d.DeudaID, out var detalle) ? detalle : null))
                .ToList();

            if (!string.IsNullOrWhiteSpace(estado))
                response = response
                    .Where(d => string.Equals(d.Estado, estado.Trim(), StringComparison.OrdinalIgnoreCase))
                    .ToList();

            if (fechaInicio.HasValue)
                response = response
                    .Where(d => d.FechaGeneracion.Date >= fechaInicio.Value.Date)
                    .ToList();

            if (fechaFin.HasValue)
                response = response
                    .Where(d => d.FechaGeneracion.Date <= fechaFin.Value.Date)
                    .ToList();

            if (!string.IsNullOrWhiteSpace(codigoPuesto))
                response = response
                    .Where(d => string.Equals(d.CodigoPuesto, codigoPuesto.Trim(), StringComparison.OrdinalIgnoreCase))
                    .ToList();

            if (!string.IsNullOrWhiteSpace(codigoDeuda))
                response = response
                    .Where(d => string.Equals(d.CodigoDeuda, codigoDeuda.Trim(), StringComparison.OrdinalIgnoreCase))
                    .ToList();

            ViewBag.Estado = estado;
            ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd");
            ViewBag.CodigoPuesto = codigoPuesto;
            ViewBag.CodigoDeuda = codigoDeuda;

            return View(response);
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

        public IActionResult Detalle(long id)
        {
            var deuda = ObtenerDeudaResponse(id);

            if (deuda == null)
                return NotFound();

            return View(deuda);
        }

        public IActionResult Exonerar(long id)
        {
            var deuda = ObtenerDeudaResponse(id);

            if (deuda == null)
                return NotFound();

            if (!string.Equals(deuda.Estado, "PENDIENTE", StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "Solo se puede exonerar una deuda con estado PENDIENTE.";
                return RedirectToAction("Index");
            }

            ViewBag.Deuda = deuda;
            return View(new DeudaExonerarRequest { DeudaID = deuda.DeudaID });
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
                ViewBag.Deuda = ObtenerDeudaResponse(request.DeudaID);
                return View(request);
            }
        }

        private void CargarListasCrear()
        {
            ViewBag.Conceptos = conceptoCobroModel.Listar();
            ViewBag.Puestos = puestoModel.Listar();
        }

        private long ObtenerUsuarioIdSesion()
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioID");

            if (!long.TryParse(usuarioId, out var id))
                throw new Exception("Usuario no autenticado.");

            return id;
        }

        private DeudaResponse? ObtenerDeudaResponse(long deudaId)
        {
            var deuda = deudaModel.BuscarPorId(deudaId);

            if (deuda == null)
                return null;

            var datosDeuda = ObtenerDatosDeuda();
            datosDeuda.TryGetValue(deudaId, out var detalle);

            return MapearDeudaResponse(deuda, detalle);
        }

        private static DeudaResponse MapearDeudaResponse(DeudaBE deuda, DeudaDetalle? detalle)
        {
            return new DeudaResponse
            {
                DeudaID = deuda.DeudaID,
                CodigoDeuda = deuda.CodigoDeuda,
                Monto = deuda.Monto,
                Estado = deuda.Estado,
                TipoGeneracion = deuda.TipoGeneracion,
                Observacion = deuda.Observacion,
                FechaGeneracion = deuda.FechaGeneracion,
                Concepto = detalle?.Concepto ?? deuda.ConceptoCobro?.Nombre ?? string.Empty,
                CodigoPuesto = detalle?.CodigoPuesto ?? deuda.Puesto?.CodigoPuesto,
                Socio = detalle?.Socio ?? deuda.Socio?.Nombres,
                FechaExoneracion = detalle?.FechaExoneracion ?? deuda.FechaExoneracion,
                MotivoExoneracion = detalle?.MotivoExoneracion ?? deuda.MotivoExoneracion,
                UsuarioGeneracion = detalle?.UsuarioGeneracion,
                UsuarioExoneracion = detalle?.UsuarioExoneracion
            };
        }

        private static Dictionary<long, DeudaDetalle> ObtenerDatosDeuda()
        {
            var datos = new Dictionary<long, DeudaDetalle>();

            using var cn = Conexion.getConnection();
            using var cmd = new SqlCommand(@"
                SELECT
                    d.deuda_id,
                    cc.nombre AS concepto,
                    p.codigo_puesto,
                    CONCAT(s.nombres, ' ', s.apellidos) AS socio,
                    d.fecha_exoneracion,
                    d.motivo_exoneracion,
                    uc.nombre_completo AS usuario_generacion,
                    ue.nombre_completo AS usuario_exoneracion
                FROM Deuda d
                INNER JOIN ConceptoCobro cc ON d.concepto_cobro_id = cc.concepto_cobro_id
                LEFT JOIN Puesto p ON d.puesto_id = p.puesto_id
                LEFT JOIN Socio s ON d.socio_id = s.socio_id
                INNER JOIN Usuario uc ON d.creado_por_usuario_id = uc.usuario_id
                LEFT JOIN Usuario ue ON d.exonerado_por_usuario_id = ue.usuario_id", cn);

            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var deudaId = Convert.ToInt64(dr["deuda_id"]);

                datos[deudaId] = new DeudaDetalle
                {
                    Concepto = dr["concepto"] == DBNull.Value ? null : dr["concepto"].ToString(),
                    CodigoPuesto = dr["codigo_puesto"] == DBNull.Value ? null : dr["codigo_puesto"].ToString(),
                    Socio = dr["socio"] == DBNull.Value ? null : dr["socio"].ToString(),
                    FechaExoneracion = dr["fecha_exoneracion"] == DBNull.Value ? null : Convert.ToDateTime(dr["fecha_exoneracion"]),
                    MotivoExoneracion = dr["motivo_exoneracion"] == DBNull.Value ? null : dr["motivo_exoneracion"].ToString(),
                    UsuarioGeneracion = dr["usuario_generacion"] == DBNull.Value ? null : dr["usuario_generacion"].ToString(),
                    UsuarioExoneracion = dr["usuario_exoneracion"] == DBNull.Value ? null : dr["usuario_exoneracion"].ToString()
                };
            }

            return datos;
        }

        private sealed class DeudaDetalle
        {
            public string? Concepto { get; set; }
            public string? CodigoPuesto { get; set; }
            public string? Socio { get; set; }
            public DateTime? FechaExoneracion { get; set; }
            public string? MotivoExoneracion { get; set; }
            public string? UsuarioGeneracion { get; set; }
            public string? UsuarioExoneracion { get; set; }
        }
    }
}
