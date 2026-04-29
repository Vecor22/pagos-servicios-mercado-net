using Mercado.BL.BE;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Mercado.DL.DALC
{
    public class DeudaDALC
    {
        private const string SpListar = "usp_Deuda_Listar";
        private const string SpBuscarPorId = "usp_Deuda_BuscarPorId";
        private const string SpBuscarPorCodigo = "usp_Deuda_BuscarPorCodigo";
        private const string SpListarPorEstado = "usp_Deuda_ListarPorEstado";
        private const string SpListarPorFecha = "usp_Deuda_ListarPorFecha";
        private const string SpListarPorCodigoPuesto = "usp_Deuda_ListarPorCodigoPuesto";
        private const string SpCrear = "usp_Deuda_Crear";
        private const string SpExonerar = "usp_Deuda_Exonerar";

        public List<DeudaBE> Listar()
        {
            List<DeudaBE> lista = new List<DeudaBE>();

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

        public DeudaBE? BuscarPorId(long deudaId)
        {
            DeudaBE? deuda = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorId, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@deuda_id", deudaId);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        deuda = Mapear(dr);
                }
            }

            return deuda;
        }

        public DeudaBE? BuscarPorCodigo(string codigoDeuda)
        {
            DeudaBE? deuda = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorCodigo, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_deuda", codigoDeuda);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        deuda = Mapear(dr);
                }
            }

            return deuda;
        }

        public List<DeudaBE> ListarPorEstado(string estado)
        {
            List<DeudaBE> lista = new List<DeudaBE>();

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpListarPorEstado, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@estado", estado);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(Mapear(dr));
                }
            }

            return lista;
        }

        public List<DeudaBE> ListarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            List<DeudaBE> lista = new List<DeudaBE>();

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpListarPorFecha, cn))
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

        public List<DeudaBE> ListarPorCodigoPuesto(string codigoPuesto)
        {
            List<DeudaBE> lista = new List<DeudaBE>();

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

        public void Crear(DeudaBE deuda)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpCrear, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@concepto_cobro_id", deuda.ConceptoCobro.ConceptoCobroID);
                cmd.Parameters.AddWithValue("@codigo_puesto", deuda.Puesto == null ? DBNull.Value : deuda.Puesto.CodigoPuesto);
                cmd.Parameters.AddWithValue("@monto", deuda.Monto);
                cmd.Parameters.AddWithValue("@tipo_generacion", deuda.TipoGeneracion);
                cmd.Parameters.AddWithValue("@observacion", (object?)deuda.Observacion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@usuario_id", deuda.CreadoPor.UsuarioID);

                cmd.ExecuteNonQuery();
            }
        }

        public void Exonerar(long deudaId, string motivo, long usuarioId)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpExonerar, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@deuda_id", deudaId);
                cmd.Parameters.AddWithValue("@motivo", motivo);
                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                cmd.ExecuteNonQuery();
            }
        }

        private static DeudaBE Mapear(SqlDataReader dr)
        {
            DeudaBE deuda = new DeudaBE
            {
                DeudaID = Convert.ToInt64(dr["deuda_id"]),
                CodigoDeuda = dr["codigo_deuda"].ToString() ?? "",
                Monto = Convert.ToDecimal(dr["monto"]),
                Estado = dr["estado"].ToString() ?? "",
                TipoGeneracion = dr["tipo_generacion"].ToString() ?? "",
                Observacion = dr["observacion"] == DBNull.Value ? null : dr["observacion"].ToString(),
                FechaGeneracion = Convert.ToDateTime(dr["fecha_generacion"]),
                FechaExoneracion = ExisteColumna(dr, "fecha_exoneracion") && dr["fecha_exoneracion"] != DBNull.Value
                    ? Convert.ToDateTime(dr["fecha_exoneracion"])
                    : null,
                MotivoExoneracion = ExisteColumna(dr, "motivo_exoneracion") && dr["motivo_exoneracion"] != DBNull.Value
                    ? dr["motivo_exoneracion"].ToString()
                    : null
            };

            if (ExisteColumna(dr, "concepto"))
            {
                deuda.ConceptoCobro = new ConceptoCobroBE
                {
                    Nombre = dr["concepto"].ToString() ?? ""
                };
            }

            if (ExisteColumna(dr, "codigo_puesto") && dr["codigo_puesto"] != DBNull.Value)
            {
                deuda.Puesto = new PuestoBE
                {
                    CodigoPuesto = dr["codigo_puesto"].ToString() ?? ""
                };
            }

            if (ExisteColumna(dr, "socio") && dr["socio"] != DBNull.Value)
            {
                deuda.Socio = new SocioBE
                {
                    Nombres = dr["socio"].ToString() ?? ""
                };
            }

            return deuda;
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
