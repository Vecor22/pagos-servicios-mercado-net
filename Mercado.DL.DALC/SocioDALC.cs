using Microsoft.Data.SqlClient;
using Mercado.BL.BE;
using System.Data;

namespace Mercado.DL.DALC
{
    public class SocioDALC
    {
        public List<SocioBE> listar()
        {
            List<SocioBE> lista = new List<SocioBE>();
            try
            {
                using (SqlConnection con = Conexion.getConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarSocio", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SocioBE socio = new SocioBE();
                        socio.IdSocio  = dr.GetInt32(0);
                        socio.Nombre   = dr.GetString(1);
                        socio.Dni      = dr.GetString(2);
                        socio.Telefono = dr.IsDBNull(3) ? "" : dr.GetString(3);
                        lista.Add(socio);
                    }
                }
            }
            catch (Exception) { }
            return lista;
        }

        public SocioBE buscar(int idSocio)
        {
            SocioBE socio = null;
            try
            {
                using (SqlConnection con = Conexion.getConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_BuscarSocio", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idSocio", idSocio);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        socio = new SocioBE();
                        socio.IdSocio  = dr.GetInt32(0);
                        socio.Nombre   = dr.GetString(1);
                        socio.Dni      = dr.GetString(2);
                        socio.Telefono = dr.IsDBNull(3) ? "" : dr.GetString(3);
                    }
                }
            }
            catch (Exception) { }
            return socio;
        }

        public int insertar(SocioBE socioBE)
        {
            int idInsertado = 0;
            try
            {
                using (SqlConnection con = Conexion.getConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_GuardarSocio", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre",   socioBE.Nombre);
                    cmd.Parameters.AddWithValue("@dni",      socioBE.Dni);
                    cmd.Parameters.AddWithValue("@telefono", socioBE.Telefono ?? "");

                    SqlParameter param = new SqlParameter("@nuevoID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    idInsertado = Convert.ToInt32(param.Value);
                }
            }
            catch (Exception) { throw; }
            return idInsertado;
        }

        public bool actualizar(SocioBE socioBE)
        {
            bool procesado = false;
            try
            {
                using (SqlConnection con = Conexion.getConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_ActualizarSocio", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idSocio",  socioBE.IdSocio);
                    cmd.Parameters.AddWithValue("@nombre",   socioBE.Nombre);
                    cmd.Parameters.AddWithValue("@dni",      socioBE.Dni);
                    cmd.Parameters.AddWithValue("@telefono", socioBE.Telefono ?? "");

                    SqlParameter param = new SqlParameter("@procesado", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    procesado = Convert.ToBoolean(param.Value);
                }
            }
            catch (Exception) { throw; }
            return procesado;
        }

        public bool eliminar(int idSocio)
        {
            bool procesado = false;
            try
            {
                using (SqlConnection con = Conexion.getConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarSocio", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idSocio", idSocio);

                    SqlParameter param = new SqlParameter("@procesado", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    procesado = Convert.ToBoolean(param.Value);
                }
            }
            catch (Exception) { throw; }
            return procesado;
        }
    }
}
