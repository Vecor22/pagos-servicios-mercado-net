using Mercado.BL.BE;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Mercado.DL.DALC
{
    public class PuestoDALC
    {
        private const string SpListar = "usp_Puesto_Listar";
        private const string SpBuscarPorId = "usp_Puesto_BuscarPorId";
        private const string SpBuscarPorCodigo = "usp_Puesto_BuscarPorCodigo";
        private const string SpCrear = "usp_Puesto_Crear";

        public List<PuestoBE> Listar()
        {
            List<PuestoBE> lista = new List<PuestoBE>();

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

        public PuestoBE? BuscarPorId(long puestoId)
        {
            PuestoBE? puesto = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorId, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@puesto_id", puestoId);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        puesto = Mapear(dr);
                    }
                }
            }

            return puesto;
        }

        public PuestoBE? BuscarPorCodigo(string codigoPuesto)
        {
            PuestoBE? puesto = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorCodigo, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_puesto", codigoPuesto);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        puesto = Mapear(dr);
                    }
                }
            }

            return puesto;
        }

        public void Crear()
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpCrear, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
            }
        }

        private static PuestoBE Mapear(SqlDataReader dr)
        {
            return new PuestoBE
            {
                PuestoID = Convert.ToInt64(dr["puesto_id"]),
                CodigoPuesto = dr["codigo_puesto"].ToString() ?? string.Empty,
                FechaCreacion = Convert.ToDateTime(dr["fecha_creacion"])
            };
        }
    }
}
