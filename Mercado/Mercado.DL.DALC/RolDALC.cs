using Microsoft.Data.SqlClient;
using Mercado.BL.BE;
using System.Data;

namespace Mercado.DL.DALC
{
    public class RolDALC
    {
        private const string SpListar = "usp_Rol_Listar";
        private const string SpBuscarPorId = "usp_Rol_BuscarPorId";
        private const string SpCrear = "usp_Rol_Crear";
        private const string SpActualizar = "usp_Rol_Actualizar";

        public List<RolBE> Listar()
        {
            List<RolBE> lista = new List<RolBE>();

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

        public RolBE? BuscarPorId(long rolId)
        {
            RolBE? rol = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorId, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rol_id", rolId);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        rol = Mapear(dr);
                    }
                }
            }

            return rol;
        }

        public void Crear(RolBE rol)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpCrear, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", rol.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", rol.Descripcion);

                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(RolBE rol)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpActualizar, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@rol_id", rol.RolID);
                cmd.Parameters.AddWithValue("@nombre", rol.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", rol.Descripcion);
                cmd.Parameters.AddWithValue("@activo", rol.Activo);

                cmd.ExecuteNonQuery();
            }
        }

        private static RolBE Mapear(SqlDataReader dr)
        {
            return new RolBE
            {
                RolID = Convert.ToInt64(dr["rol_id"]),
                Nombre = dr["nombre"].ToString() ?? string.Empty,
                Descripcion = dr["descripcion"] == DBNull.Value ? "" : dr["descripcion"].ToString() ?? "",
                Activo = Convert.ToBoolean(dr["activo"]),
                FechaCreacion = Convert.ToDateTime(dr["fecha_creacion"]),
                FechaActualizacion = dr["fecha_actualizacion"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(dr["fecha_actualizacion"])
            };
        }
    }
}
