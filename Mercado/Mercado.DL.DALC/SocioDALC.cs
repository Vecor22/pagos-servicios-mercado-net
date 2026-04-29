using Mercado.BL.BE;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Mercado.DL.DALC
{
    public class SocioDALC
    {
        private const string SpListar = "usp_Socio_Listar";
        private const string SpBuscarPorId = "usp_Socio_BuscarPorId";
        private const string SpBuscarPorNombre = "usp_Socio_BuscarPorNombre";
        private const string SpBuscarPorDni = "usp_Socio_BuscarPorDni";
        private const string SpCrear = "usp_Socio_Crear";
        private const string SpActualizar = "usp_Socio_Actualizar";
        private const string SpCambiarEstado = "usp_Socio_CambiarEstado";

        public List<SocioBE> Listar()
        {
            List<SocioBE> lista = new List<SocioBE>();

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

        public SocioBE? BuscarPorId(long id)
        {
            SocioBE? socio = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorId, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@socio_id", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        socio = Mapear(dr);
                }
            }

            return socio;
        }

        public List<SocioBE> BuscarPorNombre(string nombre)
        {
            List<SocioBE> lista = new List<SocioBE>();

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorNombre, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", nombre);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(Mapear(dr));
                }
            }

            return lista;
        }

        public SocioBE? BuscarPorDni(string dni)
        {
            SocioBE? socio = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorDni, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dni", dni);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        socio = Mapear(dr);
                }
            }

            return socio;
        }

        public void Crear(SocioBE socio)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpCrear, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombres", socio.Nombres);
                cmd.Parameters.AddWithValue("@apellidos", socio.Apellidos);
                cmd.Parameters.AddWithValue("@dni", socio.Dni);
                cmd.Parameters.AddWithValue("@correo", (object?)socio.Correo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@telefono", (object?)socio.Telefono ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(SocioBE socio)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpActualizar, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@socio_id", socio.SocioID);
                cmd.Parameters.AddWithValue("@nombres", socio.Nombres);
                cmd.Parameters.AddWithValue("@apellidos", socio.Apellidos);
                cmd.Parameters.AddWithValue("@correo", (object?)socio.Correo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@telefono", (object?)socio.Telefono ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void CambiarEstado(long socioId, string estado)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpCambiarEstado, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@socio_id", socioId);
                cmd.Parameters.AddWithValue("@estado", estado);

                cmd.ExecuteNonQuery();
            }
        }

        private static SocioBE Mapear(SqlDataReader dr)
        {
            return new SocioBE
            {
                SocioID = Convert.ToInt64(dr["socio_id"]),
                CodigoSocio = dr["codigo_socio"].ToString() ?? "",
                Nombres = dr["nombres"].ToString() ?? "",
                Apellidos = dr["apellidos"].ToString() ?? "",
                Dni = dr["dni"].ToString() ?? "",
                Correo = dr["correo"] == DBNull.Value ? null : dr["correo"].ToString(),
                Telefono = dr["telefono"] == DBNull.Value ? null : dr["telefono"].ToString(),
                Estado = dr["estado"].ToString() ?? "",
                FechaCreacion = Convert.ToDateTime(dr["fecha_creacion"]),
                FechaActualizacion = dr["fecha_actualizacion"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(dr["fecha_actualizacion"])
            };
        }
    }
}
