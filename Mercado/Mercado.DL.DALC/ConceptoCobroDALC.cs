using Mercado.BL.BE;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Mercado.DL.DALC
{
    public class ConceptoCobroDALC
    {
        private const string SpListar = "usp_ConceptoCobro_Listar";
        private const string SpBuscarPorId = "usp_ConceptoCobro_BuscarPorId";
        private const string SpCrear = "usp_ConceptoCobro_Crear";
        private const string SpActualizar = "usp_ConceptoCobro_Actualizar";

        public List<ConceptoCobroBE> Listar()
        {
            List<ConceptoCobroBE> lista = new List<ConceptoCobroBE>();

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpListar, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(Mapear(dr));
                    }
                }
            }

            return lista;
        }

        public ConceptoCobroBE? BuscarPorId(long conceptoCobroId)
        {
            ConceptoCobroBE? concepto = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorId, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@concepto_cobro_id", conceptoCobroId);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        concepto = Mapear(dr);
                    }
                }
            }

            return concepto;
        }

        public void Crear(ConceptoCobroBE concepto)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpCrear, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", concepto.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", (object?)concepto.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tipo_cobro", concepto.TipoCobro);

                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(ConceptoCobroBE concepto)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpActualizar, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@concepto_cobro_id", concepto.ConceptoCobroID);
                cmd.Parameters.AddWithValue("@nombre", concepto.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", (object?)concepto.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tipo_cobro", concepto.TipoCobro);

                cmd.ExecuteNonQuery();
            }
        }

        private static ConceptoCobroBE Mapear(SqlDataReader dr)
        {
            return new ConceptoCobroBE
            {
                ConceptoCobroID = Convert.ToInt64(dr["concepto_cobro_id"]),
                Nombre = dr["nombre"].ToString() ?? string.Empty,
                Descripcion = dr["descripcion"] == DBNull.Value ? null : dr["descripcion"].ToString(),
                TipoCobro = dr["tipo_cobro"].ToString() ?? string.Empty,
                FechaCreacion = Convert.ToDateTime(dr["fecha_creacion"]),
                FechaActualizacion = dr["fecha_actualizacion"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(dr["fecha_actualizacion"])
            };
        }
    }
}
