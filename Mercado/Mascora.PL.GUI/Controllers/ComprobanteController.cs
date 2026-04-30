using Mercado.BL.BE;
using Mercado.DL.DALC;
using Mercado.PL.GUI.DTO.Response;
using Mercado.PL.GUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Mercado.PL.GUI.Controllers
{
    public class ComprobanteController : Controller
    {
        private readonly ComprobanteModel comprobanteModel = new ComprobanteModel();

        public IActionResult Detalle(long pagoId)
        {
            var comprobante = comprobanteModel.BuscarPorIdPago(pagoId);

            if (comprobante == null)
                return NotFound();

            var detalle = ObtenerDetalleComprobante(pagoId);

            var response = new ComprobanteResponse
            {
                ComprobanteID = comprobante.ComprobanteID,
                NumeroComprobante = comprobante.NumeroComprobante,
                TipoComprobante = comprobante.TipoComprobante,
                Estado = comprobante.Estado,
                FechaEmision = comprobante.FechaEmision,
                FechaAnulacion = comprobante.FechaAnulacion,
                MotivoAnulacion = comprobante.MotivoAnulacion,
                CodigoPago = detalle?.CodigoPago ?? comprobante.Pago?.CodigoPago ?? string.Empty,
                CodigoDeuda = detalle?.CodigoDeuda ?? comprobante.Pago?.Deuda?.CodigoDeuda ?? string.Empty,
                CodigoPuesto = detalle?.CodigoPuesto ?? comprobante.Pago?.Deuda?.Puesto?.CodigoPuesto,
                Socio = detalle?.Socio ?? comprobante.Pago?.Deuda?.Socio?.Nombres,
                MontoPagado = detalle?.MontoPagado ?? comprobante.Pago?.MontoPagado ?? 0,
                MedioPago = detalle?.MedioPago ?? comprobante.Pago?.MedioPago ?? string.Empty,
                NumeroOperacion = detalle?.NumeroOperacion ?? comprobante.Pago?.NumeroOperacion,
                FechaPago = detalle?.FechaPago ?? comprobante.Pago?.FechaPago ?? DateTime.MinValue,
                RegistradoPor = detalle?.RegistradoPor,
                AnuladoPor = detalle?.AnuladoPor ?? comprobante.AnuladoPor?.NombreCompleto
            };

            return View(response);
        }

        private static ComprobanteDetalle? ObtenerDetalleComprobante(long pagoId)
        {
            using var cn = Conexion.getConnection();
            using var cmd = new SqlCommand("usp_Comprobante_BuscarPorIdPago", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pago_id", pagoId);

            using var dr = cmd.ExecuteReader();

            if (!dr.Read())
                return null;

            return new ComprobanteDetalle
            {
                CodigoPago = dr["codigo_pago"].ToString() ?? string.Empty,
                CodigoDeuda = dr["codigo_deuda"].ToString() ?? string.Empty,
                CodigoPuesto = dr["codigo_puesto"] == DBNull.Value ? null : dr["codigo_puesto"].ToString(),
                Socio = dr["socio"] == DBNull.Value ? null : dr["socio"].ToString(),
                MontoPagado = dr["monto_pagado"] == DBNull.Value ? null : Convert.ToDecimal(dr["monto_pagado"]),
                MedioPago = dr["medio_pago"] == DBNull.Value ? null : dr["medio_pago"].ToString(),
                NumeroOperacion = dr["numero_operacion"] == DBNull.Value ? null : dr["numero_operacion"].ToString(),
                FechaPago = dr["fecha_pago"] == DBNull.Value ? null : Convert.ToDateTime(dr["fecha_pago"]),
                RegistradoPor = dr["registrado_por"] == DBNull.Value ? null : dr["registrado_por"].ToString(),
                AnuladoPor = dr["anulado_por"] == DBNull.Value ? null : dr["anulado_por"].ToString()
            };
        }

        private sealed class ComprobanteDetalle
        {
            public string CodigoPago { get; set; } = string.Empty;
            public string CodigoDeuda { get; set; } = string.Empty;
            public string? CodigoPuesto { get; set; }
            public string? Socio { get; set; }
            public decimal? MontoPagado { get; set; }
            public string? MedioPago { get; set; }
            public string? NumeroOperacion { get; set; }
            public DateTime? FechaPago { get; set; }
            public string? RegistradoPor { get; set; }
            public string? AnuladoPor { get; set; }
        }
    }
}
