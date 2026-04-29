using Mercado.BL.BE;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Mercado.DL.DALC
{
    public class PagoDALC
    {
        private const string SpListar = "usp_Pago_Listar";
        private const string SpBuscarPorId = "usp_Pago_BuscarPorId";
        private const string SpBuscarPorCodigo = "usp_Pago_BuscarPorCodigo";
        private const string SpBuscarPorCodigoDeuda = "usp_Pago_BuscarPorCodigoDeuda";
        private const string SpListarPorCodigoPuesto = "usp_Pago_ListarPorCodigoPuesto";
        private const string SpListarPorFechas = "usp_Pago_ListarPorFechas";
        private const string SpCrear = "usp_Pago_Crear";
        private const string SpAnular = "usp_Pago_Anular";

        public List<PagoBE> Listar()
        {
            List<PagoBE> lista = new List<PagoBE>();

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpListar, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(Mapear(dr));
                }
            }

            return lista;
        }

        public PagoBE? BuscarPorId(long pagoId)
        {
            PagoBE? pago = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorId, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pago_id", pagoId);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        pago = Mapear(dr);
                }
            }

            return pago;
        }

        public PagoBE? BuscarPorCodigo(string codigoPago)
        {
            PagoBE? pago = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorCodigo, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_pago", codigoPago);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        pago = Mapear(dr);
                }
            }

            return pago;
        }

        public PagoBE? BuscarPorCodigoDeuda(string codigoDeuda)
        {
            PagoBE? pago = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorCodigoDeuda, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_deuda", codigoDeuda);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        pago = Mapear(dr);
                }
            }

            return pago;
        }

        public List<PagoBE> ListarPorCodigoPuesto(string codigoPuesto)
        {
            List<PagoBE> lista = new List<PagoBE>();

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpListarPorCodigoPuesto, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_puesto", codigoPuesto);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(Mapear(dr));
                }
            }

            return lista;
        }

        public List<PagoBE> ListarPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            List<PagoBE> lista = new List<PagoBE>();

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpListarPorFechas, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha_inicio", fechaInicio.Date);
                cmd.Parameters.AddWithValue("@fecha_fin", fechaFin.Date);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(Mapear(dr));
                }
            }

            return lista;
        }

        public void Crear(string codigoDeuda, string medioPago, string? numeroOperacion, long usuarioId)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpCrear, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigo_deuda", codigoDeuda);
                cmd.Parameters.AddWithValue("@medio_pago", medioPago);
                cmd.Parameters.AddWithValue("@numero_operacion", (object?)numeroOperacion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                cmd.ExecuteNonQuery();
            }
        }

        public void Anular(long pagoId, string motivo, long usuarioId)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpAnular, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pago_id", pagoId);
                cmd.Parameters.AddWithValue("@motivo", motivo);
                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                cmd.ExecuteNonQuery();
            }
        }

        private static PagoBE Mapear(SqlDataReader dr)
        {
            return new PagoBE
            {
                PagoID = Convert.ToInt64(dr["pago_id"]),
                CodigoPago = dr["codigo_pago"].ToString() ?? "",
                MontoPagado = Convert.ToDecimal(dr["monto_pagado"]),
                MedioPago = dr["medio_pago"].ToString() ?? "",
                NumeroOperacion = dr["numero_operacion"] == DBNull.Value ? null : dr["numero_operacion"].ToString(),
                Estado = dr["estado"].ToString() ?? "",
                FechaPago = Convert.ToDateTime(dr["fecha_pago"])
            };
        }
    }
}
