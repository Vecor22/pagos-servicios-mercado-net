using Microsoft.Data.SqlClient;
using Mercado.BL.BE;
using System.Data;

namespace Mercado.DL.DALC
{
    public class UsuarioDALC
    {
        private const string SpListar = "usp_Usuario_Listar";
        private const string SpBuscarPorId = "usp_Usuario_BuscarPorId";
        private const string SpBuscarPorUsername = "usp_Usuario_BuscarPorUsername";
        private const string SpCrear = "usp_Usuario_Crear";
        private const string SpActualizar = "usp_Usuario_Actualizar";
        private const string SpCambiarEstado = "usp_Usuario_CambiarEstado";
        private const string SpCambiarPassword = "usp_Usuario_CambiarPassword";
        private const string SpLogin = "usp_Auth_Login";

        public List<UsuarioBE> Listar()
        {
            List<UsuarioBE> lista = new List<UsuarioBE>();

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpListar, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(MapearUsuario(dr));
                    }
                }
            }

            return lista;
        }

        public UsuarioBE? BuscarPorId(long usuarioId)
        {
            UsuarioBE? usuario = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorId, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        usuario = MapearUsuario(dr);
                    }
                }
            }

            return usuario;
        }

        public UsuarioBE? BuscarPorUsername(string username)
        {
            UsuarioBE? usuario = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpBuscarPorUsername, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        usuario = MapearUsuario(dr);
                    }
                }
            }

            return usuario;
        }

        public UsuarioBE? Login(string username)
        {
            UsuarioBE? usuario = null;

            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpLogin, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        usuario = MapearUsuario(dr);
                    }
                }
            }

            return usuario;
        }

        public void Crear(UsuarioBE usuario)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpCrear, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", usuario.Username);
                cmd.Parameters.AddWithValue("@password_hash", usuario.PasswordHash);
                cmd.Parameters.AddWithValue("@nombre_completo", usuario.NombreCompleto);
                cmd.Parameters.AddWithValue("@foto_url", (object?)usuario.FotoUrl ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@rol_id", usuario.Rol.RolID);

                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(UsuarioBE usuario)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpActualizar, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@usuario_id", usuario.UsuarioID);
                cmd.Parameters.AddWithValue("@nombre_completo", usuario.NombreCompleto);
                cmd.Parameters.AddWithValue("@foto_url", (object?)usuario.FotoUrl ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@rol_id", usuario.Rol.RolID);

                cmd.ExecuteNonQuery();
            }
        }

        public void CambiarEstado(long usuarioId, string estado)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpCambiarEstado, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);
                cmd.Parameters.AddWithValue("@estado", estado);

                cmd.ExecuteNonQuery();
            }
        }

        public void CambiarPassword(long usuarioId, string passwordHash)
        {
            using (SqlConnection cn = Conexion.getConnection())
            using (SqlCommand cmd = new SqlCommand(SpCambiarPassword, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);
                cmd.Parameters.AddWithValue("@password_hash", passwordHash);

                cmd.ExecuteNonQuery();
            }
        }

        private static UsuarioBE MapearUsuario(SqlDataReader dr)
        {
            UsuarioBE usuario = new UsuarioBE
            {
                UsuarioID = Convert.ToInt64(dr["usuario_id"]),
                Username = dr["username"].ToString() ?? string.Empty,
                PasswordHash = ExisteColumna(dr, "password_hash") && dr["password_hash"] != DBNull.Value
                    ? dr["password_hash"].ToString() ?? string.Empty
                    : string.Empty,
                NombreCompleto = dr["nombre_completo"].ToString() ?? string.Empty,
                FotoUrl = dr["foto_url"] == DBNull.Value ? null : dr["foto_url"].ToString(),
                Estado = dr["estado"].ToString() ?? string.Empty,
                FechaCreacion = ExisteColumna(dr, "fecha_creacion") && dr["fecha_creacion"] != DBNull.Value
                    ? Convert.ToDateTime(dr["fecha_creacion"])
                    : DateTime.MinValue,
                FechaActualizacion = ExisteColumna(dr, "fecha_actualizacion") && dr["fecha_actualizacion"] != DBNull.Value
                    ? Convert.ToDateTime(dr["fecha_actualizacion"])
                    : null,
                Rol = new RolBE
                {
                    RolID = Convert.ToInt64(dr["rol_id"]),
                    Nombre = ExisteColumna(dr, "rol") ? dr["rol"].ToString() ?? string.Empty : string.Empty
                }
            };

            return usuario;
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
