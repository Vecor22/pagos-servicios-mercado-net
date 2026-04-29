using Mercado.BL.BE;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Mercado.DL.DALC
{
    public class SocioPuestoDALC
    {
        private const string SpListar = "usp_SocioPuesto_Listar";
        private const string SpCrearAsignacion = "usp_SocioPuesto_CrearAsignacion";
        private const string SpReasignar = "usp_SocioPuesto_Reasignar";
        private const string SpBuscarPorCodigoPuesto = "usp_SocioPuesto_BuscarPorCodigoPuesto";
        private const string SpListarPorNombreSocio = "usp_SocioPuesto_ListarPorNombreSocio";
        private const string SpListarPorDniSocio = "usp_SocioPuesto_ListarPorDniSocio";

        public List<SocioPuestoBE> Listar()
        {
            List<SocioPuestoBE> lista = new List<SocioPuestoBE>();

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

        public void CrearAsignacion(string codigoSocio, string codigoPuesto, long usuarioId)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpCrearAsignacion, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigo_socio", codigoSocio);
                cmd.Parameters.AddWithValue("@codigo_puesto", codigoPuesto);
                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                cmd.ExecuteNonQuery();
            }
        }

        public void Reasignar(string codigoSocio, string codigoPuesto, long usuarioId)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpReasignar, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigo_socio", codigoSocio);
                cmd.Parameters.AddWithValue("@codigo_puesto", codigoPuesto);
                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                cmd.ExecuteNonQuery();
            }
        }

        public SocioPuestoBE? BuscarPorCodigoPuesto(string codigoPuesto)
        {
            SocioPuestoBE? entidad = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorCodigoPuesto, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_puesto", codigoPuesto);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        entidad = Mapear(dr);
                    }
                }
            }

            return entidad;
        }

        public List<SocioPuestoBE> ListarPorNombreSocio(string nombre)
        {
            List<SocioPuestoBE> lista = new List<SocioPuestoBE>();

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpListarPorNombreSocio, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", nombre);

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

        public List<SocioPuestoBE> ListarPorDniSocio(string dni)
        {
            List<SocioPuestoBE> lista = new List<SocioPuestoBE>();

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpListarPorDniSocio, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dni", dni);

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

        private static SocioPuestoBE Mapear(SqlDataReader dr)
        {
            return new SocioPuestoBE
            {
                Socio = new SocioBE
                {
                    CodigoSocio = dr["codigo_socio"].ToString() ?? "",
                    Nombres = dr["nombres"].ToString() ?? "",
                    Apellidos = dr["apellidos"].ToString() ?? "",
                    Dni = dr["dni"].ToString() ?? ""
                },
                Puesto = new PuestoBE
                {
                    CodigoPuesto = dr["codigo_puesto"].ToString() ?? ""
                },
                AsignadoPor = new UsuarioBE
                {
                    NombreCompleto = dr["asignado_por"].ToString() ?? ""
                },
                FechaAsignacion = Convert.ToDateTime(dr["fecha_asignacion"])
            };
        }
    }
}
