using Mercado.BL.BE;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Mercado.DL.DALC
{
    public class ComprobanteDALC
    {
        private const string SpBuscarPorIdPago = "usp_Comprobante_BuscarPorIdPago";
        private const string SpBuscarPorNumero = "usp_Comprobante_BuscarPorNumero";
        private const string SpCrear = "usp_Comprobante_Crear";
        private const string SpAnularPorPago = "usp_Comprobante_AnularPorPago";

        public ComprobanteBE? BuscarPorIdPago(long pagoId)
        {
            ComprobanteBE? comprobante = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorIdPago, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pago_id", pagoId);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        comprobante = Mapear(dr);
                }
            }

            return comprobante;
        }

        public ComprobanteBE? BuscarPorNumero(string numeroComprobante)
        {
            ComprobanteBE? comprobante = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorNumero, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@numero_comprobante", numeroComprobante);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        comprobante = Mapear(dr);
                }
            }

            return comprobante;
        }

        public void Crear(long pagoId, long usuarioId)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpCrear, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pago_id", pagoId);
                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                cmd.ExecuteNonQuery();
            }
        }

        public void AnularPorPago(long pagoId, string motivo, long usuarioId)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpAnularPorPago, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pago_id", pagoId);
                cmd.Parameters.AddWithValue("@motivo", motivo);
                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                cmd.ExecuteNonQuery();
            }
        }

        private static ComprobanteBE Mapear(SqlDataReader dr)
        {
            ComprobanteBE comprobante = new ComprobanteBE
            {
                ComprobanteID = Convert.ToInt64(dr["comprobante_id"]),
                NumeroComprobante = dr["numero_comprobante"].ToString() ?? "",
                TipoComprobante = dr["tipo_comprobante"].ToString() ?? "",
                Estado = dr["estado"].ToString() ?? "",
                FechaEmision = Convert.ToDateTime(dr["fecha_emision"]),
                FechaAnulacion = dr["fecha_anulacion"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(dr["fecha_anulacion"]),
                MotivoAnulacion = dr["motivo_anulacion"] == DBNull.Value
                    ? null
                    : dr["motivo_anulacion"].ToString()
            };

            if (ExisteColumna(dr, "codigo_pago"))
            {
                comprobante.Pago = new PagoBE
                {
                    CodigoPago = dr["codigo_pago"].ToString() ?? "",
                    MontoPagado = ExisteColumna(dr, "monto_pagado") && dr["monto_pagado"] != DBNull.Value
                        ? Convert.ToDecimal(dr["monto_pagado"])
                        : 0,
                    MedioPago = ExisteColumna(dr, "medio_pago") ? dr["medio_pago"].ToString() ?? "" : "",
                    NumeroOperacion = ExisteColumna(dr, "numero_operacion") && dr["numero_operacion"] != DBNull.Value
                        ? dr["numero_operacion"].ToString()
                        : null,
                    FechaPago = ExisteColumna(dr, "fecha_pago") && dr["fecha_pago"] != DBNull.Value
                        ? Convert.ToDateTime(dr["fecha_pago"])
                        : DateTime.MinValue,
                    Deuda = new DeudaBE
                    {
                        CodigoDeuda = ExisteColumna(dr, "codigo_deuda")
                            ? dr["codigo_deuda"].ToString() ?? ""
                            : "",
                        Puesto = new PuestoBE
                        {
                            CodigoPuesto = ExisteColumna(dr, "codigo_puesto") && dr["codigo_puesto"] != DBNull.Value
                                ? dr["codigo_puesto"].ToString() ?? ""
                                : ""
                        },
                        Socio = new SocioBE
                        {
                            Nombres = ExisteColumna(dr, "socio") && dr["socio"] != DBNull.Value
                                ? dr["socio"].ToString() ?? ""
                                : ""
                        }
                    }
                };
            }

            if (ExisteColumna(dr, "anulado_por") && dr["anulado_por"] != DBNull.Value)
            {
                comprobante.AnuladoPor = new UsuarioBE
                {
                    NombreCompleto = dr["anulado_por"].ToString() ?? ""
                };
            }

            return comprobante;
        }

        private static bool ExisteColumna(SqlDataReader dr, string nombreColumna)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(nombreColumna, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
